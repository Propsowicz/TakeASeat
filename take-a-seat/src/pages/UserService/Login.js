/* eslint-disable */

import React, {useContext, useRef} from 'react';
import { Button } from 'primereact/button';
import { Password } from 'primereact/password';
import { InputText } from 'primereact/inputtext';
import {UserContext} from '../../context/UserContext'
import { Message } from 'primereact/message';
import { Messages } from 'primereact/messages';


const Login = () => {
    const {login} = useContext(UserContext)
    const {msg401} = useContext(UserContext)
    const {msg500} = useContext(UserContext)

    return (
        <div className="site-main-body-login">
            <form onSubmit={login} className='login-container grid'>
                <InputText placeholder='login' name='username' className='col-12 login-component'/>
                <Password placeholder='password'  feedback={false} toggleMask={true} name='password' className='col-12 l-log-comp' inputClassName='login-component'/>
                <Button label='Login' className='col-12 login-component'/>                            
            </form>
            <Messages ref={msg401} />
            <Messages ref={msg500} />   
        </div>
    );
};

export default Login;
