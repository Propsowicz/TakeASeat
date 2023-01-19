/* eslint-disable */

import React, {createContext, useEffect, useState, useRef} from "react";
import {useHistory} from 'react-router-dom'
import {url, typHeader} from '../const/constValues'
import jwt_decode from 'jwt-decode'
import { Messages } from 'primereact/messages';


export const UserContext = createContext()

export const UserContextProvider = ({children}) => {
    const navigator = useHistory().push
    const [authTokens, setAuthTokens] = useState(localStorage.getItem('JWTokens') ? JSON.parse(localStorage.getItem('JWTokens')) : [])
    const [userData, setUserData] = useState(localStorage.getItem('JWTokens') ? jwt_decode(JSON.parse(localStorage.getItem('JWTokens')).accessToken) : [])

    const WrongUsernameOrPassword = useRef(null);
    const StatusCode500 = useRef(null);
    const NotTheSamePasswords = useRef(null);
    const FillAllFields = useRef(null);
    const EmailFormNotValid = useRef(null);
    const UserRegisterServerValidationErrors = useRef(null)
    const UserAccountSuccessfullyCreated = useRef(null)

    const login = async (e) => {
        e.preventDefault()
        let response = await fetch(`${url}/api/User/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "userName": e.target.username.value,
                "password": e.target.password.value
            })            
        })
        let data = await response.json()

        if (response.status === 202){            
            setAuthTokens(data)
            setUserData(jwt_decode(data.accessToken))
            localStorage.setItem('JWTokens', JSON.stringify(data))
            navigator('/')
            window.location.reload()
        }else if (response.status === 401){
            WrongUsernameOrPassword.current.show([
                { severity: 'error', detail: 'Wrong password or username. Please try again.', life:3000 }
            ])
        }else{
            StatusCode500.current.show([
                { severity: 'error', detail: 'Internal server error. Please try again later.', life:3000 }
            ])
        }       
    }

    function isInputFilledValidator(input){
        if (input === ""){
            FillAllFields.current.show([
                { severity: 'error', detail: 'Please fill all fields.', life:3000 }
            ])
            return true
    }}
    
    function arePasswordsTheSame(pass1, pass2){
        if (pass1 !== pass2){
            NotTheSamePasswords.current.show([
                { severity: 'error', detail: 'Passwords are not the same.', life:3000 }
            ])
            return true
        }
    }    

    const register = async (e) => {
        e.preventDefault()
        let username = e.target.username.value
        let password = e.target.password.value
        let password2 = e.target.password2.value
        let firstName = e.target.firstName.value
        let lastName = e.target.lastName.value
        let email = e.target.email.value

        // validators:
        if (
        isInputFilledValidator(username) ||
        isInputFilledValidator(password) ||
        isInputFilledValidator(password2) ||
        isInputFilledValidator(firstName) ||
        isInputFilledValidator(lastName) ||
        isInputFilledValidator(email) ||
        arePasswordsTheSame(password, password2)
        ){
            return null
        }
        
        const response = await fetch(`${url}/api/User/register`,{
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "userName": username,
                "password": password,
                "firstName": firstName,
                "lastName": lastName,
                "email": email
            })
        })
        
        if (response.status === 400){
            let status = await response.json()
            status.errors.forEach(c => {
                let error = c.code.replace(/([A-Z])/g, ' $1').trim()
                UserRegisterServerValidationErrors.current.show([
                    { severity: 'error', detail: error , life:5000 }
                ])
            });
        }else if (response.status === 500){
            StatusCode500.current.show([
                { severity: 'error', detail: 'Internal server error. Please try again later.', life:3000 }
            ])
        }else{
            UserAccountSuccessfullyCreated.current.show([
                { severity: 'success', detail: "Account Has Been Successfully Created." , life:3000 }
            ])
            setTimeout(function() {
                navigator("/")
              }, 3500);
        }     
    }

    const logout = async () => {
        localStorage.clear()
        setAuthTokens([])
        setUserData([])
        navigator('/')
    }

    let refreshTokens = async () => {            
        let data = {
            'accessJWToken': JSON.parse(localStorage.getItem('JWTokens')).accessToken,
            'refreshJWToken': JSON.parse(localStorage.getItem('JWTokens')).refreshToken
        }
        let response = await fetch(`${url}/api/User/refresh`, {
            method: 'POST',
            headers: typHeader,
            body: JSON.stringify(data),
        })        
        let tokenData = await response.json()  
        if(response.status === 200){                                         
            setAuthTokens(tokenData)
            setUserData(jwt_decode(tokenData.accessJWToken))
            localStorage.setItem('JWTokens', JSON.stringify(tokenData))  
        }
        else{
            logout()                                                        
        }
    }

    useEffect(() => {
        let fourMinutes = 1000 * 14 * 60
        let interval = setInterval(() => {
            refreshTokens()
        }, fourMinutes)
        return () => clearInterval(interval)
    },[])

    const contextData = {
        login: login,
        register: register,
        logout: logout,


        userData: userData,

        // validation msg
        WrongUsernameOrPassword: WrongUsernameOrPassword,
        StatusCode500: StatusCode500,
        NotTheSamePasswords: NotTheSamePasswords,
        FillAllFields: FillAllFields,
        EmailFormNotValid: EmailFormNotValid,
        UserRegisterServerValidationErrors: UserRegisterServerValidationErrors,
        UserAccountSuccessfullyCreated: UserAccountSuccessfullyCreated,
        
    }
    return(
        <UserContext.Provider value={contextData}>
            {children}
        </UserContext.Provider>
    )
}
