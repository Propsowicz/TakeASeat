/* eslint-disable */

import React, {useEffect, useState} from 'react';
import { SeatRowComponent } from './SeatComponents/SeatRowComponent';
import {url} from '../../const/constValues'
import ReserveSeatComponent from './ReserveSeats/ReserveSeatComponent';
import { Tooltip } from 'primereact/tooltip';
import { Button } from 'primereact/button';

const DisplaySeats = (props) => {
    const [seats, setSeats] = useState([]);
    const [reservedSeatsList, setReservedSeatsList] = useState([])

    // TOOLTIP NEEDS TO BE REPAIRED

    const reserveSeats = async (e) => {    
        let price = 0
        if(e.target.offsetParent.className === 'p-button p-component seat-component'){
            e.target.offsetParent.style.background = "black"
            price = e.target.offsetParent.name
        }else if (e.target.offsetParent.className === ''){
            e.target.style.background = "black"
            price = e.target.name
        }            
        e.target.style.color = "white"


        let reservedTempSeatData = {
            "showId": parseInt(props.showId),
            "row": e.target.innerText[0],
            "position": e.target.innerText.slice(1),
            "price": price,            
        }         

        setReservedSeatsList(reservedSeatsList => [...reservedSeatsList, reservedTempSeatData])
    }

    const getSeats = async () => {
        const response = await fetch(`${url}/api/Seat/ShowId-${props.showId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (response.status == 200) {
            const data = await response.json();
            setSeats(data);
            console.log(data)
        } else {
            console.log(response.status);
            console.log(response.statusText);
        }
    };

    useEffect(() => {
        getSeats()
    }, [])

    return (
        <div>
            {reservedSeatsList.map((map) => (<p>{map.row + map.position + map.price}</p>))}
            {seats.map((row) => (
                <div className="card">
                    <div className="card-container overflow-hidden">
                        <div className="flex gap-2 seat-rows">                           
                            {row.map((seat) => (
                                <div onClick={reserveSeats} >
                                    <Button type="button" label={seat.row + seat.position} className='seat-component' 
                                    style={{'background': `${seat.seatColor}`}} name={seat.price}
                                    tooltip={`${seat.price}$`}>
                                        {/* <Tooltip target=".seat-component" autoHide={false}>
                                            <div className='seat-tooltip'>
                                                <p>Price: {seat.price}$</p>              
                                            </div>
                                        </Tooltip> */}
                                    </Button>                                    
                                </div>                
                            ))}
                        </div>                                     
                    </div>
                </div>
            ))}

        </div>
        // <div>
        //     <p>seats:</p>
        //     {seats.map((row) => (
        //         <ReserveSeatComponent row={row} showId={props.showId}/>
        //     ))}

        // </div>
    );
};

export default DisplaySeats;
