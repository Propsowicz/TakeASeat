/* eslint-disable */

import React, { useState, useEffect } from 'react';
import { Paginator } from 'primereact/paginator';
import {url, typHeader} from '../const/constValues'
import ClosestShow from '../components/home-page/ClosestShow'

const ShowsByTag = () => {
    const [pageSize, setPageSize] = useState(10);
    const [pageNumber, setPageNumber] = useState(1);
    const [showRecordsNumber, setShowRecordsNumber] = useState();
    const [first, setfirst] = useState(0);
    const [shows, setShows] = useState([]);
    const eventTagName = window.location.href.split('/')[5];

    const onPageChange = (e) => {
        setPageNumber(e.page + 1)
        setfirst(e.first)
    }

    const PaginatorTemplate = {
        layout: 'PrevPageLink PageLinks NextPageLink RowsPerPageDropdown CurrentPageReport',
        'PrevPageLink': (options) => {
            return (
                <button type="button" className={options.className} onClick={options.onClick} disabled={options.disabled}>
                    <span className="p-3">Previous</span>
                </button>
            )
        },
        'NextPageLink': (options) => {
            return (
                <button type="button" className={options.className} onClick={options.onClick} disabled={options.disabled}>
                    <span className="p-3">Next</span>
                </button>
            )
        },
    }

    const getShowsByEventTag = async () => {
        const response = await fetch(`${url}/api/Tags?PageNumber=${pageNumber}&PageSize=${pageSize}&EventTagName=${eventTagName}`, {
            method: 'GET',
            headers: typHeader,
        });

        if (response.status === 200) {
            const data = await response.json();
            console.log(data);
            setShows(data);
        }
    };


    useEffect(() => {
        getShowsByEventTag();
    }, []);

    return (
        <div className="site-main-body">
            {shows.map((show, index) => (
                <ClosestShow key={index} event={show.eventName} eventId={show.eventId} showDescr={show.description} showId={show.id} date={show.date} type={show.eventType}
                place={show.eventPlace} tags={show.eventTags} eventSlug={show.eventSlug} seatsLeft={show.seatsLeft}/>
            ))}
            <Paginator template={PaginatorTemplate} first={first} rows={pageSize} totalRecords={showRecordsNumber} onPageChange={onPageChange}></Paginator>
        </div>
    );
};

export default ShowsByTag;
