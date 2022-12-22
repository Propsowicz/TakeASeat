/* eslint-disable */

import 'primereact/resources/primereact.min.css';
import 'primeflex/primeflex.css';
import '../css/HomePage.css'
import { Button } from 'primereact/button';

import React from 'react'

export const MainNavbar = () => {
  return (
    <div className="card site-header">
        <div className="flex flex-row flex-wrap card-container yellow-container">
            <div className="flex align-items-center justify-content-center">
                <Button label="Home Page" className="p-button-text" />
            </div>
            <div className="flex align-items-center justify-content-center">
                <Button label="All Events" className="p-button-text" />
            </div>
            <div className="flex align-items-center justify-content-center">
                <Button label="Account" className="p-button-text" />
            </div>
        </div>
    </div>
  )
}
export default MainNavbar;