/* eslint-disable */

import React, {useContext, useEffect, useState, useRef} from 'react';
import {url, typHeader} from '../../const/constValues'
import {UserContext} from '../../context/UserContext'
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { InputNumber } from 'primereact/inputnumber';
import { Dropdown } from 'primereact/dropdown';
import { MultiSelect } from 'primereact/multiselect';
import { Toast } from 'primereact/toast';
import ToastYesButton from './ToastYesButton';


const CreateEvent = () => {
    const toast = useRef(null)
    const {userData} = useContext(UserContext)
    const [eventTags, setEventTags] = useState([])
    const [eventTypes, setEventTypes] = useState([])
    const [eventTypeParams, setEventTypeParams] = useState()
    const [eventTagsParams, setEventTagsParams] = useState([])
    const [createEventData, setCreateEventData] = useState()

    const getEventTags = async () => {
        const response = await fetch(`${url}/api/Tags/tags-list`, {
            method: "GET",
            headers: typHeader
        })
        if (response.status === 200){
            let data = await response.json()
            setEventTags(data)
        }else{
            console.log("error")
        }
    }

    const getEventTypes = async () => {
        const response = await fetch(`${url}/api/EventTypes`, {
            method: "GET",
            headers: typHeader
        })
        if (response.status === 200){
            let data = await response.json()
            setEventTypes(data)
        }else{
            console.log("error")
        }
    }

    function getListOfEventTypes(listOfEventTypes) {
        let tempList = []
        listOfEventTypes.forEach(type => {
            tempList.push(
                {name: type.name, id: type.id}
            )
        })
        return tempList
    }

    const onChangeEventType = (e) => {
        setEventTypeParams(e.value)
    }

    function getListOfEventTags(listOfEventTags) {
        let tempList = []
        listOfEventTags.forEach(tag => {
            tempList.push(
                {tagName: tag.tagName, id: tag.id}
            )
        })
        return tempList
    }

    const onChangeEventTag = (e) => {
        setEventTagsParams(e.value)
    }


    const createConfirmToast = (e) => {
        let eventData = 
            {
                "eventDTO" : 
                {
                "name": e.target.eventName.value,
                "duration": e.target.eventDuration.value,
                "description": e.target.eventDescription.value,
                "imageUrl": e.target.imageUrl.value,
                "place": e.target.eventPlace.value,
                "eventTypeId": eventTypeParams.id,
                "creatorId": userData.UserId,
                },
                "eventTagsDTO" : eventTagsParams
            }        
        e.preventDefault()
        toast.current.show({ severity: 'warn', sticky: true, content: (
            <div className="flex flex-column" style={{flex: '1'}}>
                <div className="text-center">
                    <i className="pi pi-exclamation-triangle" style={{fontSize: '3rem'}}></i>
                    <h4>Are you sure?</h4>
                    <p>Confirm to proceed</p>
                </div>
                <div className="grid p-fluid">
                    <div className="col-6">
                        <ToastYesButton 
                        eventData={eventData} 
                        />
                    </div>
                    <div className="col-6">
                        <Button type="button" label="No" className="p-button-secondary" onClick={NoConfirmToast}/>
                    </div>
                </div>
            </div>
        ) });
    }    

    const NoConfirmToast = () => {
        toast.current.clear();
    }

    const YesConfirmToast = async (e) => {
        NoConfirmToast()
    }

    useEffect(() => {
        getEventTags()
        getEventTypes()
    }, [])

    return (
        <div className="site-main-body-create">
            <Toast ref={toast} position="top-center"/>
            <form className='create-event-container grid' onSubmit={createConfirmToast}>
                <InputText placeholder='Event Name' name='eventName' className='col-12 create-event-component' required/>
                <InputNumber placeholder='Event Duration' name='eventDuration' className='col-12 create-event-component-number' required/>
                <InputText placeholder='Event Description' name='eventDescription' className='col-12 create-event-component' required/>
                <InputText placeholder='Event image URL' name='imageUrl' className='col-12 create-event-component' required/>
                <InputText placeholder='Event Place' name='eventPlace' className='col-12 create-event-component' required/>
                <MultiSelect inputId="multiselect" value={eventTagsParams} placeholder='Event Tags' style={{'width':'15rem'}} options={getListOfEventTags(eventTags)} optionLabel="tagName" onChange={onChangeEventTag} className='col-12 create-event-component' required/>
                <Dropdown inputId="dropdown" value={eventTypeParams} placeholder='Event Type' style={{'width':'15rem'}} options={getListOfEventTypes(eventTypes)} optionLabel="name" onChange={onChangeEventType} className='col-12 create-event-component' required/>
                <Button label='Create New Event' className='col-12 login-component' type='submit'/>
            </form>
        </div>
    );
};

export default CreateEvent;
