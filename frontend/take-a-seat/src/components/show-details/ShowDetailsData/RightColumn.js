/* eslint-disable */

import React from 'react';
import { Divider } from 'primereact/divider';
import { Chip } from 'primereact/chip';
import {dateSerializer} from '../../../utils/dateSerializer'
import {useHistory} from 'react-router-dom'

const RightColumn = (props) => {
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
        <div className="grid">
            <div className="col-12"><p className="show-details-text-descr">SHOW DESCRIPTION</p></div>
            <div className="col-12"><p>{props.description}</p></div>
            <div className="col-12"><p className="show-details-text-descr">DATE</p></div>
            <div className="col-12"><p>{dateSerializer(props.date)}</p></div>
            <div className="col-12"><p className="show-details-text-descr">PLACE</p></div>
            <div className="col-12"><p>{props.place}</p></div>
            <div className="col-12"><p className="show-details-text-descr">EVENT TYPE</p></div>
            <div className="col-12"><p>{props.type}</p></div>
            <div className="col-12"><p className="show-details-text-descr">ORGANIZER</p></div>
            <div className="col-12"><p>{props.creator}</p></div>
            <Divider type='dashed'/>
            {props.tags.map((tag, index) => (
                <Chip label={tag.eventTag.tagName} className='generic-chip' onClick={goToTagPage}/>
            ) )}
        </div>
    );
};

export default RightColumn;
