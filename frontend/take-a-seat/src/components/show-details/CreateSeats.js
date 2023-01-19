/* eslint-disable */

import React, { useEffect, useState, useRef } from 'react';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { SeatRowComponent } from './SeatComponents/SeatRowComponent';
import { Toast } from 'primereact/toast';
import {url, typHeader} from '../../const/constValues'
import { json, useHistory } from 'react-router-dom';


const CreateSeats = (props) => {
    const toast = useRef(null)
    const seatsColorDropdown = [
        {label: "Red", value: 'red'},
        {label: "Blue", value: 'blue'},
        {label: "Yellow", value: 'yellow'},
        {label: "Green", value: 'green'},
        {label: "Orange", value: 'orange'}
    ]
    const [seatColorTemp, setSeatsColorTemp] = useState()
    const [newSeats, setNewSeats] = useState([])
    const listOfRowsDefault = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K']
    const [listOfRows, setListOfRows] = useState(listOfRowsDefault)
    const [rowsCounter, setRowsCounter] = useState(0)
    const navigator = useHistory().push
    
    function parsePrice(input) {
        if (input.length < 4){return input}
        else{return input[0] + input[2] + input[3] + input[4]}
    }

    const createNewRow = (e) => {
        e.preventDefault()
        let tempListOfRows = listOfRows
        let tempTable = []
        let numOfSeats = e.target.numOfSeats.value
        let rowPrice = parsePrice(e.target.rowPrice.value)
        let rowColor = e.target.rowColor.value
                
        if (rowsCounter < listOfRowsDefault.length){
            for(let i = 0; i < numOfSeats; i++){
                tempTable.push({
                    "showId": props.showId,
                    "row": tempListOfRows[0],
                    "position": i + 1,
                    "price": rowPrice,
                    "seatColor": rowColor
                })
            }
            setNewSeats(newSeats => [...newSeats, tempTable])
        }else{
            console.log(`You can't create more rows than ${listOfRowsDefault.length}.`)
        }  

        tempListOfRows.splice(0, 1)
        setListOfRows(tempListOfRows)        
        setRowsCounter(rowsCounter + 1)
    }    

    const CreateSeats = async () => {
        const tempSeatTable = []
        newSeats.forEach(row => {
            row.forEach(seat => {
                tempSeatTable.push(seat)
            });
        });        
        console.log(typHeader)
        const response = await fetch(`${url}/api/Seats/create-multiple`, {
            method: "POST",
            headers: typHeader,
            body: JSON.stringify(tempSeatTable)
        })            
        if(response.status != 201) {
            console.log("error")            
        }else{
            navigator('/')
        }  
    }

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
        CreateSeats()        
    }

    useEffect(() => {
        console.log(newSeats)
    },[newSeats])

    return (
        <div>
            <Toast ref={toast} position="top-center"/>

            <div className="card">
                <div className="card-container overflow-hidden">
                    <div>
                        {newSeats.map((row) => (
                            <SeatRowComponent row={row}/>
                        ))}
                    </div>                    
                        <div className="flex gap-2"> 
                            <form onSubmit={createNewRow} className="flex gap-2 add-seat-form">     
                                <div className="flex align-items-center justify-content-center"><InputNumber name='numOfSeats' min={1} max={20} placeholder="Number of seats in a row" tooltip='Between 1 and 40 seats.' required/></div>
                                <div className="flex align-items-center justify-content-center"><InputNumber name='rowPrice'  min={0} max={2000} placeholder="Tickets price in a row" tooltip='Between 0 and 2000$' currency='USD' required/></div>
                                <div className="flex align-items-center justify-content-center"><Dropdown name='rowColor' value={seatColorTemp} options={seatsColorDropdown} onChange={(e) => setSeatsColorTemp(e.value)} placeholder="Select seats color" required/></div>
                                <div className="flex align-items-center justify-content-center"><Button label="Add new row" /></div>
                            </form>    
                        </div>    
                        <div className="flex flex-row-reverse">
                            <Button label="Create audience" onClick={showConfirmToast}/>   
                        </div> 
                                    
                </div>
            </div>
        </div>
    );
};

export default CreateSeats;
