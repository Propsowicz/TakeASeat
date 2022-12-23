/* eslint-disable */
import { Panel } from 'primereact/panel';
import { Divider } from 'primereact/divider';
import { Chip } from 'primereact/chip';


import React, {useState} from 'react'
import PanelMainInfo from './PanelMainInfo';

const ClosestShow = (props) => {
    const [eventTags, setEventTags] = useState(props.tags)
  return (
    <div className='show-panel' >
        <Panel header={props.event} id={props.key}>
            <PanelMainInfo showDescr={props.showDescr} date={props.date} place={props.place} eventId={props.eventId} showId={props.showId}/>           
            <Divider type='dashed'/>
            {eventTags.map((tag, index) => (
                <Chip label={tag.eventTag.tagName} className='generic-chip'/>
            ) )}
        </Panel>

    </div>
  )
}

export default ClosestShow
