/* eslint-disable */

import React, { useContext, useEffect, useState } from 'react';
import { UserContext } from '../context/UserContext';
import {url, typHeader} from '../const/constValues'
import OrderData from '../components/payment/OrderData'
import OrderSummary from '../components/payment/OrderSummary';
import { ProgressSpinner } from 'primereact/progressspinner';


const Payment = () => {
    const {userData} = useContext(UserContext)
    const [orderData, setOrderData] = useState([])
    const [paymentData, setPaymentData] = useState()

    const getOrderData = async () => {
        const response = await fetch(`${url}/api/Order?UserId=${userData.UserId}`, {
            method: "GET",
            headers: typHeader            
        })
        if (response.status !== 200){
            console.log("error")
        }else {
            const data = await response.json()
            console.log(data)
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
        }else {
            const data = await response.json()
            console.log(data)
            setPaymentData(data)
        }
    }

    useEffect(() => {
        console.log(paymentData)
        console.log(userData)
        getOrderData()
        getPaymentData()
    },[])

    return (
        <div className="site-main-body">
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
                <p>You don't have any orders..</p>
            }
            {/* <p>Your order:</p>
            <OrderData data={orderData}/>
            <br></br>
            <p>Summary:</p>
            {paymentData
            ?
                <OrderSummary data={paymentData} userName={userData.UserName} firstName={userData.FirstName} lastName={userData.LastName} email={userData.Email}/>
            :
                <ProgressSpinner/>
            } */}
            
        </div>
    );
};

export default Payment;
