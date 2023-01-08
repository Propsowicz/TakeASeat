
/* eslint-disable */

import React, {useState, useEffect, useContext} from 'react';
import {url, typHeader} from '../../const/constValues'
import {UserContext} from '../../context/UserContext'
import {Link, useNavigate} from 'react-router-dom'
import { Accordion, AccordionTab } from 'primereact/accordion';
import AccordionComponent from '../../components/events/AccordionComponent';
import { Button } from 'primereact/button';
import { Panel } from 'primereact/panel';
import CreateEvent from '../../components/organizator-components/CreateEvent';
import { Divider } from 'primereact/divider';
import {dateSerializer, dateUTCSerializer} from '../../utils/dateSerializer'
import { InputText } from 'primereact/inputtext';
import { Calendar } from 'primereact/calendar';


const OrganizatorPanel = () => {
    const [eventsData, setEventsData] = useState([]);
    const navigate = useNavigate()
    const {userData} = useContext(UserContext)
    const [showDate, setShowDate] = useState(null);


    const getEvents = async () => {
        const response = await fetch(`${url}/api/Events/by-user?userName=${userData.UserName}`, {
        //const response = await fetch(`${url}/api/Events/by-user?userName=LOG`, {
            method: "GET",
            headers: typHeader
        })
        if (response.status === 200){
            let data = await response.json()
            setEventsData(data)
            console.log(data)
        }
    }

    const deleteEvent = async (e) => {
        let eventId = e.target.name
        const response = await fetch(`${url}/api/Event/delete`, {
                method: "POST",
                headers: typHeader,
                body: JSON.stringify({
                    "eventId": eventId
                })
            })        
        if (response.status === 200){
            window.location.reload()
        }
    }

    const createShow = async (e) => {
        e.preventDefault()        
        let showData = {
            "eventId": e.target.eventId.value,
            "date": dateUTCSerializer(e.target.showDate.value),
            "description": e.target.showDescription.value
        }
        const response = await fetch(`${url}/api/Show/create`, {
            method: "POST",
            headers: typHeader,
            body: JSON.stringify(showData)
        })        
        if (response.status === 201){
            window.location.reload()
        }        
    }
    
    const deleteShow = async (e) => {
        let eventId = e.target.name
        const response = await fetch(`${url}/api/Show/delete`, {
                method: "POST",
                headers: typHeader,
                body: JSON.stringify({
                    "showId": eventId
                })
            })        
        if (response.status === 200){
            window.location.reload()
        }
    }

    useEffect(() => {
        getEvents()
        console.log(userData)
    }, [])
  return (
    <div className="site-main-body-created-events">
        <Panel header={`Event Creation Panel`} >
            <CreateEvent />
        </Panel>
        <Panel className='event-creation-panel' header={'Events created by You.'}>
            {eventsData.map((event) => (                
                <div className='grid'>
                    <div className='grid col-12'>
                        <Link to={`/edit/${event.id}/`} className='col-11 grid link-button'>
                            <div className='col-12 md:col-12 lg:col-4'>
                                <p className='col-12 md:col-12 lg:col-12'>Name</p>
                                <p className='col-12 md:col-12 lg:col-12'>{event.name}</p>
                            </div>
                            <div className='col-12 md:col-12 lg:col-4'>
                                <p className='col-12 md:col-12 lg:col-12'>Description</p>
                                <div className='col-12 md:col-12 lg:col-12'>{event.description}</div>
                            </div>
                            <div className='col-12 md:col-12 lg:col-2'>
                                <p className='col-12 md:col-12 lg:col-12'>Duration [min]</p>
                                <div className='col-12 md:col-12 lg:col-12'>{event.duration}</div>
                            </div>
                            <div className='col-12 md:col-12 lg:col-2'>
                                <p className='col-12 md:col-12 lg:col-12'>Place</p>
                                <div className='col-12 md:col-12 lg:col-12'>{event.place}</div>
                            </div>                     
                        </Link>
                        <button onClick={deleteEvent} name={event.id} className='col-1 organizator-panel-delete-btn'>x</button>
                    </div>
                    <Divider type='dashed'/> 
                    {event.shows.map((show) => (
                        <div className='grid col-12'>
                            <Link className='col-11 grid link-button' name={show.Id}>
                                <span className='col-12 md:col-12 lg:col-4'>{show.description}</span>
                                <span className='col-12 md:col-12 lg:col-4'>{dateSerializer(show.date)}</span>                                                
                            </Link>
                            <button onClick={deleteShow} name={show.id} className='col-1 organizator-panel-delete-btn'>x</button>
                        </div>
                    ))}
                    <Divider type='dashed'/> 
                    <div className='grid col-12'>
                        <p className='col-12'>Create new show:</p>
                        <form className='col-12 grid' onSubmit={createShow}>
                            <input name="eventId" value={event.id} hidden></input>
                            <InputText name='showDescription' placeholder='Show description' className='col-12 md:col-12 lg:col-8'/>
                            <Calendar id="time24" value={showDate} onChange={(e) => setShowDate(e.value)} showTime  name='showDate' placeholder='Date' className='col-12 md:col-12 lg:col-2 calendar-component'/>
                            <Button className='col-12 md:col-12 lg:col-2' label="Add Show" type='submit'/>
                        </form>                        
                    </div>
                    <Divider type='dashed'/> 
                </div>
            ))}
        </Panel>        
    </div>
  )
}

export default OrganizatorPanel