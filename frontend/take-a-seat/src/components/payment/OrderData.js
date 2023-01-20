/* eslint-disable */

import React, { useEffect, useState } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import {dateSerializer} from '../../utils/dateSerializer'
import { Button } from 'primereact/button';
import {url, typHeader} from '../../const/constValues'


const OrderData = (props) => {   

    const deleteReservation = async (e) => {
        const response = await fetch(`${url}/api/SeatsReservation/delete`, {
            method: "POST",
            headers: typHeader,
            body: JSON.stringify({
                "seatReservationId": e.target.name
            })
        })
        if (response.status != 204){
            console.log('error')
        }
        window.location.reload()
    }

    const removeReservationFromSeat = async (e) => {
        const response = await fetch(`${url}/api/Seats/remove-reservation`, {
            method: "POST",
            headers: typHeader,
            body: JSON.stringify({
                "seatId": e.target.name
            })
        })
        if (response.status != 204){
            console.log('error')
        }
        window.location.reload()
    }

    const headerTemplate = (props) => {
        return (                                 
                <div className="grid div-full-width">   
                    <Button type="button" className='seat-reservation-remove-button col-1' tooltip='remove order' name={props.seatReservation.id} onClick={deleteReservation}>X</Button>                 
                    <div className="col-7 align-items-center justify-content-center font-bold text-green-500 border-round ">{props.show.event.name} : {props.show.description}</div>
                    <div className="col-4 align-items-center justify-content-center font-bold text-green-500 border-round ">{dateSerializer(props.show.date)}</div>                    
                </div>                    
        );
    }

    const deleteButton = (rowData) => {
        return <Button type="button" className='seat-reservation-remove-button' 
        tooltip='remove from the order' name={rowData.id} onClick={removeReservationFromSeat}>X</Button>;
    }

    const seatCoords = (rowData) => {
        return <span>{rowData.row}{rowData.position}</span>
    }

    const priceWithCurrency = (rowData) => {
        return <span>{rowData.price}$</span>
    }

    useEffect(() => {

    },[])

    return (
        <div>
            <div className="order-summary-cart">
                
                <DataTable value={props.data} rowGroupMode="subheader" groupRowsBy="seatReservation.id"
                    sortMode="single" sortField="seatReservation.id" sortOrder={1} scrollable scrollHeight="500px"
                    rowGroupHeaderTemplate={headerTemplate} responsiveLayout="scroll" style={{"min-width": '850px'}} className="order-table">
                    <Column body={seatCoords} header="SEAT" style={{ maxWidth: '300px' }}></Column>                   
                    <Column body={priceWithCurrency} header="PRICE" style={{ minWidth: '614px' }}></Column>
                    <Column body={deleteButton} value="x"/>
                    
                </DataTable>
            </div>





        </div>
    );
};

export default OrderData;
