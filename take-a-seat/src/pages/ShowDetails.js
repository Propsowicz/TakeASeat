/* eslint-disable */
import React, { useState, useEffect } from 'react';
import CreateSeats from '../components/show-details/CreateSeats';
import ShowDescription from '../components/show-details/ShowDescription';
import {url} from '../const/constValues'
import { Panel } from 'primereact/panel';
import { Fieldset } from 'primereact/fieldset';


const ShowDetails = () => {    
    const showId = window.location.href.split('/')[5]
    
    const [seats, setSeats] = useState([])

    
    const getSeats = async () => {
      let response = await fetch (`${url}/api/Seat/ShowId-${showId}`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json'
        }
      })
      if (response.status == 200){
        let data = await response.json()
        setSeats(data)
      }else{
        console.log(response.status) 
        console.log(response.statusText) 
      }
    }

    useEffect (() => {
        getSeats()
    }, [])

    return (
      <div className='site-main-body'>        
        <ShowDescription showId={showId}/>   
        <Fieldset className='scene-comp'>
          <p className='scene-comp-descr'>EVENT SCENE</p>
        </Fieldset> 
        <CreateSeats showId={showId}/>
      </div>
      );
};

export default ShowDetails;
