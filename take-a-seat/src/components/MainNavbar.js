/* eslint-disable */

import 'primereact/resources/primereact.min.css';
import 'primeflex/primeflex.css';
import '../css/Main.css'
import { Button } from 'primereact/button';
import { Link, useNavigate } from 'react-router-dom';
import { Badge } from 'primereact/badge';
import {UserContext} from '../context/UserContext'
import {url, typHeader} from '../const/constValues'

import React, {useState, useContext, useEffect} from 'react'

export const MainNavbar = () => {
    const [costSummary, setCostSummary] = useState(0)
    const {userData} = useContext(UserContext)
    const {logout} = useContext(UserContext)
    const navigate = useNavigate()

    const getTotalCostByUser = async () => {
        if (userData.UserId){
            const response = await fetch(`${url}/api/Payment/total-user-cost?UserId=${userData.UserId}`, {
                method: "GET",
                headers: typHeader
            })
            if (response.status === 200) {
                let data = await response.json()
                setCostSummary(data.totalCost)
            }
        }        
    }

    const getCostSummary = (costSummary) => {
        return `${costSummary}$`
    }

    const goToHomePage = () => {
        navigate(`/`)
     }

    const goToLogin = () => {
        navigate('/login/')
    }

    const goToPayment = () => {
        navigate('/payment/')
    }

    const goToEvents = () => {
        navigate('/events/')
    }

    useEffect(() => {
        getTotalCostByUser()
    }, [])

  return (
    <div className="card site-header">
        <div className="flex flex-row flex-wrap card-container justify-content-between">
            <div className='flex justify-content-start'>
                <div className="flex align-items-center justify-content-center">
                <Button label="Home" className="p-button-text" onClick={goToHomePage}/>
                </div>
                <div className="flex align-items-center justify-content-center">
                    <Button label="Events" className="p-button-text" onClick={goToEvents}/>
                </div>
            </div>

            {
                userData.UserId
                ? 
                <div className='flex justify-content-end'>                
                    <div className="flex align-items-center justify-content-center">
                        <Button label="Payment" className="p-button-text" onClick={goToPayment}>
                            <Badge value={getCostSummary(costSummary)} severity="danger"/>
                        </Button>
                    </div>
                    <div className="flex align-items-center justify-content-center">
                        <Button label="Logout" className="p-button-text" onClick={logout}/>
                    </div>
                </div>
                :
                <div className='flex justify-content-end'>       
                    <div className="flex align-items-center justify-content-center">
                        <Button label="Login" className="p-button-text" onClick={goToLogin}/>
                    </div>
                </div>
            }

            
            
        </div>
    </div>
  )
}
export default MainNavbar;