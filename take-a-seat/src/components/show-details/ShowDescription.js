/* eslint-disable */

import React, {useEffect, useState} from 'react';
import {url} from '../../const/constValues'
import { Panel } from 'primereact/panel';
import { ProgressSpinner } from 'primereact/progressspinner';
import { Divider } from 'primereact/divider';

import RightColumn from './ShowDetailsData/RightColumn';
import LeftColumn from './ShowDetailsData/LeftColumn';

const ShowDescription = (props) => {  
    return (
      <div className='show-detail-panel show-panel' >        
        <Panel header={`${props.eventName}`} >
          <p className='event-description'>{props.description}</p>
          <Divider type='dashed'/> 
          <div className='grid'>
            <div className='col-12 md:col-12 lg:col-6'>
              <LeftColumn imageUrl={props.imageUrl}/>
            </div>
        
            <div className='col-12 md:col-12 lg:col-6'>
              <RightColumn description={props.description} date={props.date} place={props.place} 
              type={props.type} creator={props.creator}
              eventDescription={props.eventDescription} tags={props.tags}/>              
            </div>
          </div>                    
        </Panel>             
      </div>
    );
};

export default ShowDescription;
