/* eslint-disable */

import React, {useEffect, useState, useContext, useRef} from 'react';
import { SeatRowComponent } from './SeatComponents/SeatRowComponent';
import {url} from '../../const/constValues'
import { Tooltip } from 'primereact/tooltip';
import { Button } from 'primereact/button';
import ReservedSeats from './ReserveSeats/ReservedSeats';
import {UserContext} from '../../context/UserContext'
import { useNavigate } from 'react-router-dom';
import { Toast } from 'primereact/toast';


const DisplaySeats = (props) => {
    const [seats, setSeats] = useState([]);
    const [reservedSeatsList, setReservedSeatsList] = useState([])
    const {userData} = useContext(UserContext)
    const toast = useRef(null)
    const navigator = useNavigate()

    const reserveSeats = async (e) => { 
        
        if (e.target.className.substr(e.target.className.length-23) === 'seat-component-reserved') {
            e.target.classList.remove('seat-component-reserved')
            let tempTable = reservedSeatsList        
            for(let i = 0;i < tempTable.length;i++){
                if(tempTable[i].row === e.target.innerText[0] && tempTable[i].position === e.target.innerText.slice(1)){
                    tempTable.splice(i, 1)                    
                }
            }
            setReservedSeatsList(reservedSeatsList => [...tempTable])

        }else{
            e.target.classList.add('seat-component-reserved')

            let reservedTempSeatData = {
                "id": e.target.title,
                "row": e.target.innerText[0],
                "position": e.target.innerText.slice(1),
                "price": e.target.name,                
            }         
            setReservedSeatsList(reservedSeatsList => [...reservedSeatsList, reservedTempSeatData])
        }  
    }

    const getPrice = () => {
        let totalPrice = 0
        reservedSeatsList.forEach(seat => {
            totalPrice += parseInt(seat.price)
        });
        return totalPrice
    }

    const getSeats = async () => {
        const response = await fetch(`${url}/api/Seats/ShowId-${props.showId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (response.status == 200) {
            const data = await response.json();
            setSeats(data);
        } else {
            console.log(response.status);
            console.log(response.statusText);
        }
    };

    const showConfirmToast = () => {
        toast.current.show({ severity: 'warn', sticky: true, content: (
            <div className="flex flex-column" style={{flex: '1'}}>
                <div className="text-center">
                    <i className="pi pi-exclamation-triangle" style={{fontSize: '3rem'}}></i>
                    <h4>Are you sure?</h4>
                    <p>Confirm to proceed</p>
                </div>
                <div className="grid p-fluid">
                    <div className="col-6">
                        <Button type="button" label="Yes" className="p-button-success" onClick={YesConfirmToast}/>
                    </div>
                    <div className="col-6">
                        <Button type="button" label="No" className="p-button-secondary" onClick={NoConfirmToast}/>
                    </div>
                </div>
            </div>
        ) });
    }    

    const NoConfirmToast = () => {
        toast.current.clear();
    }

    const YesConfirmToast = () => {
        NoConfirmToast()
        postOrder()        
    }


    const postOrder = async () => {
        let buyerId = userData.UserId
        let eventId = props.eventId
        let result = {
            "userId": buyerId,            
            "seats": reservedSeatsList            
        }
        console.log(result)
        let response = await fetch(`${url}/api/SeatsReservation/create`,{
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(result)
        })
        if (response.status !== 202){
            console.log("error")
        }else{
            navigator('/payment/')
        }        
    }


    useEffect(() => {
        getSeats()
        getPrice()
    }, [])

    return (
        <div>          
            <Toast ref={toast} position="top-center"/>

            {seats.map((row) => (
                <div className="card">
                    <div className="card-container overflow-hidden">
                        <div className="flex gap-2 seat-rows">                           
                            {row.map((seat) => (
                                <div>
                                    <button type="button" className={`text-500 seat-component bg-${seat.seatColor}`}
                                    name={seat.price} onClick={reserveSeats} title={seat.id}> 
                                        {seat.row + seat.position}
                                    </button>   
                                    {/* <Tooltip target=".seat-component" autoHide={true}>
                                        <div className='seat-tooltip'>
                                            <p>Price: {seat.price}$</p>              
                               
                                            </div>
                                    </Tooltip> */}                                    
                                </div>                
                            ))}
                        </div>                                     
                    </div>
                </div>
            ))}
            <div className="flex gap-2 seat-rows">
                <span>Reserved seats:</span>
                {/* {reservedSeatsList.map((seat) => (
                    //<ReservedSeats row={seat.row} position={seat.position}/>
                    // <button type="button" className={'seat-component text-500'} key={seat.inx}>
                    //     {seat.row + seat.position}
                    // </button>
                ))} */}
                <ReservedSeats list={reservedSeatsList}/>
                <span>{getPrice()}$</span>
                <Button label="Make an order" onClick={showConfirmToast}/>
            </div>

        </div>
        
    );
};

export default DisplaySeats;