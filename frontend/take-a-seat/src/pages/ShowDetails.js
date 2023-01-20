/* eslint-disable */
import React, { useState, useEffect, useContext } from 'react';
import CreateSeats from '../components/show-details/CreateSeats';
import ShowDescription from '../components/show-details/ShowDescription';
import {url, typHeader} from '../const/constValues'
import { Panel } from 'primereact/panel';
import { Fieldset } from 'primereact/fieldset';
import { ProgressSpinner } from 'primereact/progressspinner';
import DisplaySeats from '../components/show-details/DisplaySeats';
import {UserContext} from '../context/UserContext'


const ShowDetails = () => {    
    const showId = window.location.href.split('/')[5]
    const eventSlug = window.location.href.split('/')[4]
    const {userData} = useContext(UserContext)

    const [showDetails, setShowDetails] = useState([])
    
    const getShow = async () => {
      let response = await fetch (`${url}/api/Show/${showId}`, {
          method: 'GET',
          headers: typHeader
      })
      if (response.status == 200){
          let data = await response.json()
          setShowDetails(data)
      }else{
          console.log(response.status) 
          console.log(response.statusText) 
      }               
  }
    const checkIfUserIsOrganizer = () => {
      let userRole = userData["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]      
      if (userRole) {        
          return userRole.includes("Administrator") || userRole.includes("Organizer");
      }
      return false;
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
          {
            showDetails.isReadyToSell
            ? 
              <p className='scene-comp-descr'>EVENT SCENE</p>
            :
              <p className='scene-comp-descr'>TICKETS AVAIBLE SOON!</p>
          }
          
        </Fieldset> 
        {showDetails.isReadyToSell
          ?          
          <DisplaySeats eventId={showDetails.event.id} eventSlug={eventSlug} showId={showId}/>
          :          
          <div>
              {
                checkIfUserIsOrganizer()
                ?
                  <CreateSeats showId={showId}/>
                :
                  <div></div>
              }
          </div>
          
        }        
      </div>
      );
};

export default ShowDetails;
