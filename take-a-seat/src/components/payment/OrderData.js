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
        console.log(e)

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
                    <div className="col-7 align-items-center justify-content-center font-bold text-green-500 border-round ">{props.show.event.name} : {props.show.description}</div>
                    <div className="col-4 align-items-center justify-content-center font-bold text-green-500 border-round ">{dateSerializer(props.show.date)}</div>
                    <Button type="button" className='seat-reservation-remove-button col-1' tooltip='remove from the order' name={props.seatReservation.id} onClick={deleteReservation}>X</Button>
                </div>                    
        );
    }

    const actionBodyTemplate = (rowData) => {
        return <Button type="button" className='seat-reservation-remove-button' 
        tooltip='remove from the order' name={rowData.id} onClick={removeReservationFromSeat}>X</Button>;
    }

    console.log(props.data)
    useEffect(() => {
        console.log(props.data)
    },[])

    return (
        <div>
            <div className="card">
                
                <DataTable value={props.data} rowGroupMode="subheader" groupRowsBy="seatReservation.id"
                    sortMode="single" sortField="seatReservation.id" sortOrder={1} scrollable scrollHeight="500px"
                    rowGroupHeaderTemplate={headerTemplate} responsiveLayout="scroll" style={{"width": '850px'}}>
                    <Column field="row" header="ROW" style={{ maxWidth: '100px' }}></Column>
                    <Column field="position" header="COLUMN" style={{ maxWidth: '300px' }}></Column>
                    <Column field="price" header="PRICE"  style={{ minWidth: '260px' }}></Column>
                    <Column body="USD" header="CURRENCY"  style={{ minWidth: '254px' }}></Column>
                    <Column body={actionBodyTemplate} value="x"/>
                    
                </DataTable>
            </div>





        </div>
    );
};

export default OrderData;
