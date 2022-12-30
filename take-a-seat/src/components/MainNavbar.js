/* eslint-disable */

import 'primereact/resources/primereact.min.css';
import 'primeflex/primeflex.css';
import '../css/Main.css'
import { Button } from 'primereact/button';
import { Link, useNavigate } from 'react-router-dom';

import React from 'react'

export const MainNavbar = () => {
    const navigate = useNavigate()
    const goToHomePage = () => {
        navigate(`/`)
     }

    const goToLogin = () => {
        navigate('/login/')
    }

    const goToPayment = () => {
        navigate('/payment/')
    }
  return (
    <div className="card site-header">
        <div className="flex flex-row flex-wrap card-container yellow-container">
            <div className="flex align-items-center justify-content-center">
                <Button label="Home Page" className="p-button-text" onClick={goToHomePage}/>
            </div>
            <div className="flex align-items-center justify-content-center">
                <Button label="All Events" className="p-button-text" />
            </div>
            <div className="flex align-items-center justify-content-center">
                <Button label="Payment" className="p-button-text" onClick={goToPayment}/>
            </div>
            <div className="flex align-items-center justify-content-center">
                <Button label="Login" className="p-button-text" onClick={goToLogin}/>
            </div>
        </div>
    </div>
  )
}
export default MainNavbar;