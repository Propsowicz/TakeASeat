/* eslint-disable */

import React, {useEffect, useState} from 'react';
import {url} from '../../const/constValues'
import { Panel } from 'primereact/panel';
import { ProgressSpinner } from 'primereact/progressspinner';
import { Divider } from 'primereact/divider';

import RightColumn from './ShowDetailsData/RightColumn';
import LeftColumn from './ShowDetailsData/LeftColumn';

const ShowDescription = (props) => {

  const [showDetails, setShowDetails] = useState([])

  const getShow = async () => {
      let response = await fetch (`${url}/api/Show/${props.showId}`, {
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

  useEffect(() => {
    getShow()
  }, [])

    return (
      <div className='show-detail-panel show-panel' >
        {showDetails.event 
        ?
        <Panel header={`${showDetails.event.name}`} >
          <p className='event-description'>{showDetails.event.description}</p>
          <Divider type='dashed'/> 
          <div className='grid'>
            <div className='col-12 md:col-12 lg:col-6'>
              <LeftColumn imageUrl={showDetails.event.imageUrl}/>
            </div>
            <div className='col-12 md:col-12 lg:col-6'>
              <RightColumn description={showDetails.description} date={showDetails.date} place={showDetails.event.place} 
              type={showDetails.event.eventType.name} creator={`${showDetails.event.creator.firstName} ${showDetails.event.creator.lastName}`}
              eventDescription={showDetails.event.description} tags={showDetails.event.eventTags}/>
            </div>
          </div>                    
        </Panel>
        :
        <ProgressSpinner/>
        }        
      </div>
    );
};

export default ShowDescription;
