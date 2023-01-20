/* eslint-disable */

import React, {useState, useEffect} from 'react';
import {Link, useHistory} from 'react-router-dom'
import {url, typHeader} from '../const/constValues'
import { Chip } from 'primereact/chip';

const MainFooter = () => {
    const [eventTags, setEventTags] = useState([])
    const navigator = useHistory().push

    const getEventTags = async () => {
        const response = await fetch(`${url}/api/Tags/tags-list`, {
            method: "GET",
            headers: typHeader
        })
        if (response.status === 200) {
            let data = await response.json()
            setEventTags(data)
        }
    }

    const goToTagPage = (e) => {
        let pathString = window.location.href.split('/')[4];
        let tagName = e.target.innerText  
        navigator(`/tags/${tagName.substring(1)}/`)
        if (pathString === 'tags'){          
          window.location.reload()
        }
      }

    useEffect(() => {
        getEventTags()
    }, [])
    
    return (
        <div className="footer">
            <div className="grid footer-container">
                <div className="col-12 md:col-12 lg:col-4 gap-3" >
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum dui eros, lacinia ac fermentum sit amet, vestibulum eu tellus. Praesent eu leo sit amet nisi ultrices rhoncus. Curabitur sit amet semper ligula. Pellentesque viverra dui ac purus imperdiet, non tincidunt tellus ornare. Nullam ac egestas mauris, et rutrum mi. Curabitur in tempus leo, nec hendrerit enim. Vivamus quis facilisis est. Pellentesque et molestie dui. Ut vehicula pretium ligula, a rutrum lorem laoreet vel. Mauris venenatis, libero id egestas imperdiet, neque ipsum feugiat magna, at eleifend lorem mauris at sem.</p>
                </div>
                <div className="col-12 md:col-12 lg:col-4 gap-3">
                    <ul className='list-none'>
                        <li className='ul-item'><a href='https://www.facebook.com'>Facebook</a></li>
                        <li className='ul-item'><a href='https://www.instagram.com/'>Instagram</a></li>
                        <li className='ul-item'><a href='https://twitter.com/'>Twitter</a></li>
                        <li className='ul-item'><a href='https://www.linkedin.com/'>LinkedIn</a></li>                    
                    </ul>
                </div>
                <div className="col-12 md:col-12 lg:col-4 gap-3">
                    <div>
                        {eventTags.map((tag) => (
                            <Chip label={tag.tagName} className='generic-chip footer-chip' onClick={goToTagPage}/>
                        ))}
                    </div>
                </div>
            </div>

        </div>
    );
};

export default MainFooter;
