/* eslint-disable */

import React from 'react';
import { Button } from 'primereact/button';
import {url, typHeader} from '../../const/constValues'
import {useHistory} from 'react-router-dom'

const ToastYesButtonEditEvent = (props) => {
    const navigate = useHistory().push
    console.log(JSON.stringify(props.eventData));
    
    const editEvent = async () => {
        const response = await fetch(`${url}/api/Event/edit-with-tags`, {
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
    <button type="button" className="p-button-label p-button p-component p-button-secondary p-button-success" style={{'justifyContent': 'center'}} onClick={editEvent}>Yes</button>
    )
}

export default ToastYesButtonEditEvent