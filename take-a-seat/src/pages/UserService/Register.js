/* eslint-disable */

import React, {useContext} from 'react';
import {UserContext} from '../../context/UserContext'
import { InputText } from 'primereact/inputtext';
import { Password } from 'primereact/password';
import { Button } from 'primereact/button';
import { Messages } from 'primereact/messages';

const Register = () => {
    const {register} = useContext(UserContext)
    const {StatusCode500} = useContext(UserContext)
    const {NotTheSamePasswords} = useContext(UserContext)
    const {FillAllFields} = useContext(UserContext)
    const {EmailFormNotValid} = useContext(UserContext)
    const {UserRegisterServerValidationErrors} = useContext(UserContext)
    
    return (
        <div className="site-main-body-register">
            <form className='login-container grid' onSubmit={register}>
                <InputText placeholder='username' name='username' className='col-12 login-component'/>
                <Password placeholder='password'  feedback={false} toggleMask={true} name='password' className='col-12 l-log-comp' inputClassName='login-component'/>
                <Password placeholder='repeat password'  feedback={false} toggleMask={true} name='password2' className='col-12 l-log-comp' inputClassName='login-component'/>
                <InputText placeholder='first name' name='firstName' className='col-12 login-component'/>
                <InputText placeholder='last name' name='lastName' className='col-12 login-component'/>
                <InputText placeholder='email' name='email' className='col-12 login-component'/>
                <Button label='Sign Up' className='col-12 login-component' type='submit'/>

            </form>
            <Messages ref={StatusCode500} />
            <Messages ref={FillAllFields} />
            <Messages ref={NotTheSamePasswords} />
            <Messages ref={EmailFormNotValid} />
            <Messages ref={UserRegisterServerValidationErrors} />
        </div>
    );
};

export default Register;
