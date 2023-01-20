/* eslint-disable */

import React, { useContext, useEffect, useState } from 'react';
import { UserContext } from '../context/UserContext';
import {url, typHeader} from '../const/constValues'
import OrderData from '../components/payment/OrderData'
import OrderSummary from '../components/payment/OrderSummary';
import { ProgressSpinner } from 'primereact/progressspinner';
import { Panel } from 'primereact/panel';
import {Link, useHistory} from 'react-router-dom'
import { Button } from 'primereact/button';

const Payment = () => {
    const {userData} = useContext(UserContext)
    const [orderData, setOrderData] = useState([])
    const [paymentData, setPaymentData] = useState()
    const navigator = useHistory().push

    const getOrderData = async () => {
        const response = await fetch(`${url}/api/Order?UserId=${userData.UserId}`, {
            method: "GET",
            headers: typHeader            
        })
        if (response.status !== 200){
            console.log("error")
        }else {
            const data = await response.json()
            setOrderData(data)
        }
    }    
    const getPaymentData = async () => {
        const response = await fetch(`${url}/api/Payment?UserId=${userData.UserId}`,{
            method: "GET",
            headers: typHeader 
        })
        if (response.status !== 200){
            console.log("error")
        }else{
            const data = await response.json()
            setPaymentData(data)
        }
    }

    const goToShows = () => {
        navigator('/shows/')
    }

    useEffect(() => {        
        getOrderData()
        getPaymentData()
    },[])

    return (
        <div className="site-main-body-payment">
            {
                orderData[0]
                ?
                <div>
                    <p>Your order:</p>
                    <OrderData data={orderData}/>
                    <br></br>
                    <p>Summary:</p>
                    {paymentData
                    ?
                        <OrderSummary data={paymentData} userName={userData.UserName} firstName={userData.FirstName} lastName={userData.LastName} email={userData.Email}/>
                    :
                        <ProgressSpinner/>
                    }
                </div>
                :
                <Panel header="You dont have any orders..">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                        Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.
                        Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat
                        cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
                        <div className='flex flex-row-reverse'>
                            <Button label="Check our shows.." onClick={goToShows}/>   
                        </div>                        
                </Panel>
            }           
            
        </div>
    );
};

export default Payment;
