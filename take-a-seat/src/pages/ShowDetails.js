/* eslint-disable */
import React, { useState, useEffect } from 'react';
import CreateSeats from '../components/show-details/CreateSeats';
import ShowDescription from '../components/show-details/ShowDescription';
import {url} from '../const/constValues'
import { Panel } from 'primereact/panel';
import { Fieldset } from 'primereact/fieldset';
import { ProgressSpinner } from 'primereact/progressspinner';
import DisplaySeats from '../components/show-details/DisplaySeats';


const ShowDetails = () => {    
    const showId = window.location.href.split('/')[5]
    
    const [showDetails, setShowDetails] = useState([])
    
    const getShow = async () => {
      let response = await fetch (`${url}/api/Show/${showId}`, {
          method: 'GET',
          headers: {
              'Content-Type': 'application/json'
          }
      })
      if (response.status == 200){
          let data = await response.json()
          setShowDetails(data)
      }else{
          console.log(response.status) 
          console.log(response.statusText) 
      }               
  }

    useEffect (() => {
        getShow()
    }, [])

    return (
      <div className='site-main-body'>      
        {
          showDetails.event
          ?
            <ShowDescription description={showDetails.description} date={showDetails.date} place={showDetails.event.place} 
            type={showDetails.event.eventType.name} creator={`${showDetails.event.creator.firstName} ${showDetails.event.creator.lastName}`}
            eventDescription={showDetails.event.description} tags={showDetails.event.eventTags} name={showDetails.name} 
            imageUrl={showDetails.event.imageUrl} eventName={showDetails.event.name}            
            />
          :
            <ProgressSpinner />
        }
           
        <Fieldset className='scene-comp'>
          <p className='scene-comp-descr'>EVENT SCENE</p>
        </Fieldset> 
        {showDetails.isReadyToSell
          ?
          <DisplaySeats showId={showId}/>
          :
          <CreateSeats showId={showId}/>
        }        
      </div>
      );
};

export default ShowDetails;
