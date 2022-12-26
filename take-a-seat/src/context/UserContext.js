/* eslint-disable */

import React, {createContext, useEffect, useState, useRef} from "react";
import {useNavigate} from 'react-router-dom'
import {url} from '../const/constValues'
import jwt_decode from 'jwt-decode'
import { Messages } from 'primereact/messages';


export const UserContext = createContext()

export const UserContextProvider = ({children}) => {
    const navigate = useNavigate()
    const [authTokens, setAuthTokens] = useState(localStorage.getItem('JWTokens') ? JSON.parse(localStorage.getItem('JWTokens')) : [])
    const [userData, setUserData] = useState(localStorage.getItem('JWTokens') ? jwt_decode(JSON.parse(localStorage.getItem('JWTokens')).accessToken) : [])

    const msg401 = useRef(null);
    const msg500 = useRef(null);

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
            navigate('/')
        }else if (response.status === 401){
            msg401.current.show([
                { severity: 'error', detail: 'Wrong password or username. Please try again.', life:3000 }
            ])
        }else{
            msg500.current.show([
                { severity: 'error', detail: 'Internal server error. Please try again later.', life:3000 }
            ])
        }       
    }
    useEffect(() => {
        
    },[])

    const contextData = {
        login: login,


        userData: userData,
        msg401: msg401,
        msg500: msg500,
    }
    return(
        <UserContext.Provider value={contextData}>
            {children}
        </UserContext.Provider>
    )
}
