/* eslint-disable */

import React from 'react';
import { Button } from 'primereact/button';
import {url, typHeader} from '../../const/constValues'
import {useHistory} from 'react-router-dom'


const ToastYesButton = (props) => {
    const navigate = useHistory().push
    
    const createEvent = async () => {
        const response = await fetch(`${url}/api/Event/create-with-tags`, {
            method: "POST",
            headers: typHeader,
            body: JSON.stringify(props.eventData)
        })
         
        if (response.status === 201){
            console.log('event created')
            navigate("/")
        }else{
            console.log("event creation error")
        }
    }
    

    return (
        <button type="button" className="p-button-label p-button p-component p-button-secondary p-button-success" style={{'justifyContent': 'center'}} onClick={createEvent}>Yes</button>
    );
};

export default ToastYesButton;
