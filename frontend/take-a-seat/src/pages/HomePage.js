/* eslint-disable */
import React, { useState, useEffect } from 'react';
import ClosestShow from '../components/home-page/ClosestShow';
import Landing from '../components/home-page/Landing';
import {url, typHeader} from '../const/constValues'
import { ProgressSpinner } from 'primereact/progressspinner';
import { Button } from 'primereact/button';
import {useHistory} from 'react-router-dom'

const HomePage = () => {
    const [shows, setShows] = useState([])
    const navigator = useHistory().push

    const getShows = async () => {
        let response = await fetch (`${url}/api/Shows/home-page/`, {
            method: 'GET',
            headers: typHeader
        })
        if (response.status == 200){
            let data = await response.json()
            setShows(data)
            console.log(data)
        }else{
            console.log(response.status) 
            console.log(response.statusText) 
        }      
    }

    const goToShows = () => {
        navigator("/shows/")
    }

    useEffect (() => {
        getShows()
    }, [])

    return (
        <div className='site-main-body'>
            
            <Landing />
            {shows.map((show, index) => (
                <ClosestShow key={index} event={show.eventName} eventId={show.eventId} showDescr={show.description} showId={show.id} date={show.date} type={show.eventType}
                place={show.eventPlace} tags={show.eventTags} eventSlug={show.eventSlug} seatsLeft={show.seatsLeft} isReadyToSell={show.isReadyToSell}/>
            ))}
            <div className='flex flex-row-reverse'>
                <Button label="Check All Shows" onClick={goToShows}/>
            </div>
            
            
            
        </div>
    );
};

export default HomePage;
