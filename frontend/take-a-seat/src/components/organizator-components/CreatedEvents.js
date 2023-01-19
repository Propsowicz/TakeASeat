
/* eslint-disable */

import React, {useState, useEffect, useContext} from 'react';
import {url, typHeader} from '../../const/constValues'
import {UserContext} from '../../context/UserContext'
import {Link, useHistory} from 'react-router-dom'
import { Accordion, AccordionTab } from 'primereact/accordion';
import AccordionComponent from '../events/AccordionComponent';
import { Button } from 'primereact/button';

const CreatedEvents = () => {
    const [eventsData, setEventsData] = useState([]);
    const [showsData, setShowsData] = useState([]);
    const navigate = useHistory().push
    const {userData} = useContext(UserContext)

    const getEvents = async () => {
        const response = await fetch(`${url}/api/Events/by-user?userName=${userData.UserName}`, {
            method: "GET",
            headers: typHeader
        })
        if (response.status === 200){
            let data = await response.json()
            setEventsData(data)
            console.log(data)
        }
    }

    const getShowsByEvent = async (e) => {
        let eventId = ''
        if (e.target.parentElement.parentElement.id) {
            eventId = e.target.parentElement.parentElement.id
        }else{
            eventId = e.target.parentElement.parentElement.parentElement.id
        }
        
        const response = await fetch(`${url}/api/Shows/eventId-${eventId}`, {
            method: "GET",
            headers: typHeader
        })
        if (response.status == 200){
            const data = await response.json()
            setShowsData(data)
        }
    }

    useEffect(() => {
        getEvents()
        console.log(userData)
    }, [])
    return (
        <div className="site-main-body-created-events">
            <p>Events created by user: {userData.UserName}</p>
            
                {eventsData.map((eventData) => ( 
                    <div className='grid'>     
                    <Button className='col-12 md:col-12 lg:col-2'  label='Add Show'/>
                    <Button className='col-12 md:col-12 lg:col-2'  label='Add Show'/>
                    <Button className='col-12 md:col-12 lg:col-2'  label='Add Show'/>
                        <Accordion className='col-12 md:col-12 lg:col-12'>              
                            <AccordionTab header={`${eventData.name}`} id={eventData.id} onClick={getShowsByEvent} tabIndex={eventData.id}>
                                {showsData.map((show) => (
                                    <AccordionComponent description={show.description} date={show.date} 
                                    seatsLeft={show.seatsLeft} eventSlug={show.eventSlug} id={show.id}/> 
                                    
                                ))}
                            </AccordionTab>
                        </Accordion>
                        
                    </div>
                ))}
            
        </div>
    );
};

export default CreatedEvents;
