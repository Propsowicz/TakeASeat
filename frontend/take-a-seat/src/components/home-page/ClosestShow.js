/* eslint-disable */
import { Panel } from 'primereact/panel';
import { Divider } from 'primereact/divider';
import { Chip } from 'primereact/chip';
import { Button } from 'primereact/button';


import React, {useState} from 'react'
import PanelMainInfo from './PanelMainInfo';
import {useHistory} from 'react-router-dom'

const ClosestShow = (props) => {
    const [eventTags, setEventTags] = useState(props.tags)
    const navigator = useHistory().push

    const goToTagPage = (e) => {
      let pathString = window.location.href.split('/')[4];
      let tagName = e.target.innerText  
      navigator(`/tags/${tagName.substring(1)}/`)
      if (pathString === 'tags'){
        
        window.location.reload()
      }
    }

  return (
    <div className='show-panel' >
        <Panel header={props.event} id={props.key}>
            <PanelMainInfo showDescr={props.showDescr} date={props.date} place={props.place} eventId={props.eventId} showId={props.showId} eventSlug={props.eventSlug}
                            seatsLeft={props.seatsLeft} isReadyToSell={props.isReadyToSell}/>           
            <Divider type='dashed'/>
            {eventTags.map((tag, index) => (
                <Chip label={tag} className='generic-chip' onClick={goToTagPage}/>
            ) )}
        </Panel>

    </div>
  )
}

export default ClosestShow
