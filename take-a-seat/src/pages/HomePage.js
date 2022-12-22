/* eslint-disable */
import React, { useState, useEffect } from 'react';
import ClosestShow from '../components/home-page/ClosestShow';
import Landing from '../components/home-page/Landing';

const HomePage = () => {
    const [closeShows, setCloseShows] = useState([])

    const getShows = async () => {
        let response = await fetch (`https://localhost:7252/api/Show`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        if (response.status == 200){
            let data = await response.json()
            setCloseShows(data)
            console.log(data)
        }else{
            console.log(response.status) 
            console.log(response.statusText) 
        }       
        
    }

    useEffect (() => {
        getShows()
    }, [])

    return (
        <div className='site-main-body'>
            <Landing />
            {closeShows.map((show, index) => (
                <ClosestShow key={index} event={show.event.name} eventId={show.event.id} showDescr={show.description} showId={show.id} date={show.date} type={show.event.eventType.name}
                place={show.event.place} tags={show.event.eventTags}/>
            ))}
        </div>
    );
};

export default HomePage;