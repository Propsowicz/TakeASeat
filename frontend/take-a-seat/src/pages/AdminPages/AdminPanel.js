/* eslint-disable */

import React, { useState, useEffect, useContext } from 'react';
import { url, typHeader } from '../../const/constValues';
import { UserContext } from '../../context/UserContext';
import { Checkbox } from 'primereact/checkbox';
import { Divider } from 'primereact/divider';
import { Button } from 'primereact/button';
import { Paginator } from 'primereact/paginator';
import { InputText } from 'primereact/inputtext';


const AdminPanel = () => {
    const [usersList, setUsersList] = useState([]);
    const [roles, setRoles] = useState([]);

    const [pageSize, setPageSize] = useState(20);
    const [pageNumber, setPageNumber] = useState(1);
    const [searchString, setSearchString] = useState("")
    const [userRecordsNumber, setUserRecordsNumber] = useState();
    const [first, setfirst] = useState(0);
    
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

    const onSearchStringKeyUp = (e) => {
        setSearchString(e.target.value)
    }

    const getUsers = async () => {
        let urlEndPoint = `${url}/api/Administrator/users?PageNumber=${pageNumber}&PageSize=${pageSize}`
        if (searchString.length > 0){
            urlEndPoint += `&SearchString=${searchString}`            
        }
        const response = await fetch(urlEndPoint, {
            method: 'GET',
            headers: typHeader,
        });
        if (response.status === 200) {
            const data = await response.json();
            console.log(data);
            setUsersList(data);
        } else {
            console.log('error');
        }
    };

    const getRoles = async () => {
        const response = await fetch(`${url}/api/Administrator/roles`, {
            method: 'GET',
            headers: typHeader,
        });
        if (response.status === 200) {
            const data = await response.json();
            console.log(data);
            setRoles(data);
        } else {
            console.log('error');
        }
    };

    const getUsersNumber = async () => {
        const response = await fetch(`${url}/api/Administrator/records-number`, {
            method: 'GET',
            headers: typHeader,
        });
        if (response.status === 200) {
            const data = await response.json();
            console.log(data);
            setUserRecordsNumber(data.recordsNumber);
        } else {
            console.log('error');
        }
    };
   
    function isChecked (roleObj, list){
        for(let i = 0; i < list.length; i++){
            if(list[i].name === roleObj.name){ return true}
        }
        return false
    }

    function isCheckedTitle (roleObj, list){
        for(let i = 0; i < list.length; i++){
            if(list[i].name === roleObj.name){ return 1}
        }
        return 0
    }

    const onChangeCheckbox = (e) => {
        let tempList = [...usersList]
        
        if (e.target.checked) {
            tempList[e.target.id].userRoles.push({id: e.target.value, name: e.target.name})
        }else{
            let indexToRemove = tempList[e.target.id].userRoles.indexOf({id: e.target.value, name: e.target.name})
            tempList[e.target.id].userRoles.splice(indexToRemove, 1)
        }                
        setUsersList(tempList)
    } 

    const postEditUserRoles = async (e) => {
        e.preventDefault()
        let data = {
            "userId": e.target.userId.value,
            "userRoles": []
        }
        if (e.target.User.title === "1"){ data.userRoles.push({"id": e.target.User.value, "name": e.target.User.name})}
        if (e.target.Organizer.title === "1"){ data.userRoles.push({"id": e.target.Organizer.value, "name": e.target.Organizer.name})}
        if (e.target.Administrator.title === "1"){ data.userRoles.push({"id": e.target.Administrator.value, "name": e.target.Administrator.name})}
        console.log(data)
        console.log(JSON.stringify(data))
        const response = await fetch(`${url}/api/Administrator/edit-roles`, {
            method: 'POST',
            headers: typHeader,
            body: JSON.stringify(data)
        })
        console.log(response)
    }

    function getFullName(fname, lname) {
        return `${fname[0]}. ${lname}`
    }

    useEffect(() => {
        getUsersNumber()
        getRoles();
        getUsers();
    }, [pageNumber, searchString]);


    return (
        <div className="site-main-body-created-events">
            <span className="p-float-label">
                <InputText id="inputtext" style={{'width':'15rem'}} onKeyUp={onSearchStringKeyUp}/>
                <label htmlFor="inputtext">Search</label>
            </span>
            <div className='grid' style={{'marginTop': '2rem'}}>
                <span className='col-12 md:col-12 lg:col-2'>UserName</span>
                <span className='col-12 md:col-12 lg:col-2'>Last Name</span>
                <span className='col-12 md:col-12 lg:col-4'>Email</span>
                <span className='col-12 md:col-12 lg:col-1'>User</span>           
                <span className='col-12 md:col-12 lg:col-1'>Admin</span>           
                <span className='col-12 md:col-12 lg:col-1'>Organizer</span>                               
            </div>
            <Divider />
            {usersList.map((user, index) => (
                <form className='grid admin-panel-user-div' onSubmit={postEditUserRoles}>
                    <input value={user.id} name='userId' hidden></input>
                    <span className='col-12 md:col-12 lg:col-2'>{user.userName}</span>
                    <span className='col-12 md:col-12 lg:col-2'>{getFullName(user.firstName, user.lastName)}</span>
                    <span className='col-12 md:col-12 lg:col-4'>{user.email}</span>
                    <span className='col-12 md:col-12 lg:col-3 grid'>
                        {roles.map((role) => (
                            <div className="field-checkbox col-12 md:col-12 lg:col-4" >
                                <input type='checkbox' inputId="role" id={index} name={role.name} value={role.id} checked={isChecked(role, user.userRoles)}
                                onChange={onChangeCheckbox} title={isCheckedTitle(role, user.userRoles)}/>
                            </div>
                        ))}                                       
                    </span>
                    <Button className='col-12 md:col-12 lg:col-1 save-btn' label="save" type='submit'/>   
                </form>
            ))}
            <Paginator template={PaginatorTemplate} first={first} rows={pageSize} totalRecords={userRecordsNumber} onPageChange={onPageChange}></Paginator>
        </div>
    );
};

export default AdminPanel;
