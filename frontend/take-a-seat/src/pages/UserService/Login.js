/* eslint-disable */

import React, {useContext, useRef} from 'react';
import { Button } from 'primereact/button';
import { Password } from 'primereact/password';
import { InputText } from 'primereact/inputtext';
import {UserContext} from '../../context/UserContext'
import { Message } from 'primereact/message';
import { Messages } from 'primereact/messages';
import {Link} from 'react-router-dom'


const Login = () => {
    const {login} = useContext(UserContext)
    const {WrongUsernameOrPassword} = useContext(UserContext)
    const {StatusCode500} = useContext(UserContext)

    return (
        <div className="site-main-body-login">
            <form onSubmit={login} className='login-container grid'>
                <InputText placeholder='login' name='username' className='col-12 login-component'/>
                <Password placeholder='password'  feedback={false} toggleMask={true} name='password' className='col-12 l-log-comp' inputClassName='login-component'/>
                <Button label='Login' className='col-12 login-component'/>                            
            </form>
            <Messages ref={WrongUsernameOrPassword} />
            <Messages ref={StatusCode500} />
            <div>
                <p>Don't have an account yet? <Link to={'/register/'}>Sign Up.</Link></p>
            </div>   
            
        </div>
    );
};

export default Login;
