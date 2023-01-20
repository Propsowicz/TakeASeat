/* eslint-disable */

import React from 'react'
import { Accordion, AccordionTab } from 'primereact/accordion';
import {Link} from 'react-router-dom'
import ShowDescription from '../home-page/PanelInfoData/ShowDescription';
import ShowDate from '../home-page/PanelInfoData/ShowDate';
import ShowAvaibleTickets from '../home-page/PanelInfoData/ShowAvaibleTickets';

const AccordionComponent = (props) => {
    
  return (
    <Link className='grid show-by-event-container' to={`/${props.eventSlug}/${props.id}`}>
        <div className='col-12 md:col-12 lg:col-6'>
            <ShowDescription showDescr={props.description}/>
        </div>
        <div className='col-12 md:col-12 lg:col-4'>
            <ShowDate date={props.date}/>
        </div>
        <div className='col-12 md:col-12 lg:col-2'>
            <ShowAvaibleTickets seatsLeft={props.seatsLeft} isReadyToSell={props.isReadyToSell}/>
        </div>
    </Link>       
  )
}

export default AccordionComponent