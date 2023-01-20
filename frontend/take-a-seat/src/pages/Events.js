/* eslint-disable */

import React, { useEffect, useState } from 'react';
import { Paginator } from 'primereact/paginator';
import {url, typHeader} from '../const/constValues'
import { Accordion, AccordionTab } from 'primereact/accordion';
import AccordionComponent from '../components/events/AccordionComponent';
import { ProgressSpinner } from 'primereact/progressspinner';
import {Link} from 'react-router-dom'
import {dateSerializer} from '../utils/dateSerializer'
import { InputText } from 'primereact/inputtext';
import { MultiSelect } from 'primereact/multiselect';
import { Dropdown } from 'primereact/dropdown';


const Events = () => {    
    const [searchString, setSearchString] = useState("")
    const [sortOrder, setSortOrder] = useState(null)
    const [eventTypesParams, setEventTypesParams] = useState([])
    const [eventTypesList, setEventTypesList] = useState([])

    const [pageSize, setPageSize] = useState(5);
    const [pageNumber, setPageNumber] = useState(1);
    const [showRecordsNumber, setShowRecordsNumber] = useState();
    const [first, setfirst] = useState(0);
    
    const [eventsData, setEventsData] = useState([]);
    const [showsData, setShowsData] = useState([]);

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

    const getEventTypes = async () => {
        const response = await fetch(`${url}/api/EventTypes`, {
            method: "GET",
            headers: typHeader
        })

        if (response.status == 200){
            const data = await response.json()
            setEventTypesList(data)
        }
    }

    function getListOfEventTypes(listOfEventTypes) {
        let tempList = []
        listOfEventTypes.forEach(type => {
            tempList.push(
                {name: type.name, code: type.name}
            )
        })
        return tempList
    }
    
    const onChangeEventType = (e) => {
        setEventTypesParams(e.value)
    }

    const sortingOptions = [
        {name: "By Name", code: "name"},
        {name: "By Name Descending", code: "-name"}
    ]

    const onChangeSort = (e) => {
        setSortOrder(e.value)
    }

    const onSearchStringKeyUp = (e) => {
        setSearchString(e.target.value)
    }

    const getEvents = async () => {
        let urlEndPoint = `${url}/api/Events?PageNumber=${pageNumber}&PageSize=${pageSize}`
        if (searchString.length > 0){
            urlEndPoint += `&SearchString=${searchString}`            
        }
        if (sortOrder !== null) {
            urlEndPoint += `&OrderBy=${sortOrder.code}`
        }
        if (eventTypesParams.length > 0){
            eventTypesParams.forEach(type => {
                urlEndPoint += `&EventTypes=${type.name}`
            });
        }
        const response = await fetch(urlEndPoint, {
            method: "GET",
            headers: typHeader
        })
        if (response.status == 200){
            const data = await response.json()
            setEventsData(data)
        }
    }

    const getEventsNumber = async () => {
        const response = await fetch(`${url}/api/Events/records-number`, {
            method: "GET",
            headers: typHeader
        })
        if (response.status == 200){
            const data = await response.json()
            setShowRecordsNumber(data)
        }
    }

    const getShowsByEvent = async (e) => {
        let eventId = ''
        if (e.target.parentElement.parentElement.id) {
            eventId = e.target.parentElement.parentElement.id
        }else{
            eventId = e.target.parentElement.parentElement.parentElement.id
        }
        
        const response = await fetch(`${url}/api/Shows/eventId-${eventId}`, {
            method: "GET",
            headers: typHeader
        })
        if (response.status == 200){
            const data = await response.json()
            console.log(data)
            setShowsData(data)
        }
    }

    useEffect(() => {
        getEventTypes()
        getEventsNumber()
        getEvents()

    }, [eventTypesParams, searchString, sortOrder, pageNumber])

    return (
        <div className="site-main-body-events">
            <div className='grid event-params-bar'>
                <div className='col-12 md:col-12 lg:col-4'>   
                    <span className="p-float-label">
                        <InputText id="inputtext" style={{'width':'15rem'}} onKeyUp={onSearchStringKeyUp}/>
                        <label htmlFor="inputtext">Search</label>
                    </span>
                </div>
                <div className='col-12 md:col-12 lg:col-4'>
                    <span className="p-float-label">
                        <Dropdown inputId="dropdown" style={{'width':'15rem'}} options={sortingOptions} optionLabel="name" onChange={onChangeSort}/>
                        <label htmlFor="dropdown">Sort</label>
                    </span>
                </div>
                <div className='col-12 md:col-12 lg:col-4'>
                    <span className="p-float-label">
                        <MultiSelect inputId="multiselect" value={eventTypesParams} style={{'width':'15rem'}} options={getListOfEventTypes(eventTypesList)} optionLabel="name" onChange={onChangeEventType}/>
                        <label htmlFor="multiselect">Event Types</label>
                    </span>
                </div>
            </div>
            {eventsData[0]
                ?                
            <Accordion >
                {eventsData.map((eventData) => (                    
                    <AccordionTab header={`${eventData.name}`} id={eventData.id} onClick={getShowsByEvent} tabIndex={eventData.id}>
                        {showsData.map((show) => (
                            <AccordionComponent description={show.description} date={show.date} 
                            seatsLeft={show.seatsLeft} eventSlug={show.eventSlug} id={show.id} isReadyToSell={show.isReadyToSell}/>
                        ))}
                    </AccordionTab>
                ))}
            </Accordion>
                :
                <ProgressSpinner />
            }

            
            

            <Paginator template={PaginatorTemplate} first={first} rows={pageSize} totalRecords={showRecordsNumber} onPageChange={onPageChange}></Paginator>

        </div>
    );
};

export default Events;
