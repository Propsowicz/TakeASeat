/* eslint-disable */

import React, { useContext, useEffect, useState } from 'react';
import { UserContext } from '../context/UserContext';
import {url, typHeader} from '../const/constValues'
import OrderData from '../components/payment/OrderData'

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
        getOrderData()
        getPaymentData()
    },[])

    return (
        <div className="site-main-body">
            <OrderData data={orderData}/>
        </div>
    );
};

export default Payment;
