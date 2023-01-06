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
import { ProgressSpinner } from 'primereact/progressspinner';
import ToastYesButtonEditEvent from '../../components/organizator-components/ToastYesButtonEditEvent'

const EditEventData = () => {
    const toast = useRef(null)
    const {userData} = useContext(UserContext)
    const [eventTags, setEventTags] = useState([])
    const [eventTypes, setEventTypes] = useState([])
    const [eventTypeParams, setEventTypeParams] = useState()
    const [eventTagsParams, setEventTagsParams] = useState([])
    const [createEventData, setCreateEventData] = useState()

    const eventId = window.location.href.split('/')[5]
    const [eventData, setEventData] = useState()

    const getEventData = async () => {
        const response = await fetch(`${url}/api/Event/${eventId}`, {
            method: "GET",
            headers: typHeader
        })
        if (response.status === 200) {
            let data = await response.json()
            setEventData(data)
            setEventTypeParams({name: String(data.eventType.name), id: data.eventType.id})

            let tempList = []
            data.eventTags.forEach(tag => {
                tempList.push({tagName: String(tag.eventTag.tagName), id: tag.eventTagId})
            });
            setEventTagsParams(tempList)
        }else {
            console.log("error")
        }
    }

    const getEventTags = async () => {
        const response = await fetch(`${url}/api/Tags/tags-list`, {
            method: "GET",
            headers: typHeader
        })
        if (response.status === 200){
            let data = await response.json()
            console.log(data)
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
            console.log(data)
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
        console.log(e.value)
        setEventTagsParams(e.value)
    }


    const createConfirmToast = (e) => {
        console.log(e)
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
                "id": e.target.eventId.value,
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
                        <ToastYesButtonEditEvent     
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
        console.log(createEventData)

    }

    useEffect(() => {
        getEventData()
        getEventTags()
        getEventTypes()
    }, [])


  return (
    <div className="site-main-body-edit">
        <Toast ref={toast} position="top-center"/>

        {eventData
        ?
        <form className='create-event-container grid' onSubmit={createConfirmToast}>
            <input name='eventId' value={eventData.id} hidden={true}></input>
            <InputText defaultValue={eventData.name} tooltip='Event Name' name='eventName' className='col-12 create-event-component' required/>
            <InputNumber value={eventData.duration} tooltip='Event Duration' name='eventDuration' className='col-12 create-event-component-number' required/>
            <InputText defaultValue={eventData.description} tooltip='Event Description' name='eventDescription' className='col-12 create-event-component' required/>
            <InputText defaultValue={eventData.imageUrl} tooltip='Event image URL' name='imageUrl' className='col-12 create-event-component' required/>
            <InputText defaultValue={eventData.place} tooltip='Event Place' name='eventPlace' className='col-12 create-event-component' required/>
            <MultiSelect inputId="multiselect" value={eventTagsParams} tooltip='Event Tags' style={{'width':'15rem'}} options={getListOfEventTags(eventTags)} optionLabel="tagName" onChange={onChangeEventTag} className='col-12 create-event-component' required/>
            <Dropdown inputId="dropdown" value={eventTypeParams} tooltip='Event Type' style={{'width':'15rem'}} options={getListOfEventTypes(eventTypes)} optionLabel="name" onChange={onChangeEventType} className='col-12 create-event-component' required/>
            <Button label='Edit Event Data' className='col-12 login-component' type='submit'/>
        </form>
        :
            <ProgressSpinner />
        }
        
        
    </div>
  )
}

export default EditEventData