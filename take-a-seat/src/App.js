/* eslint-disable */

import 'primereact/resources/themes/lara-dark-purple/theme.css';
import 'primereact/resources/primereact.min.css';
import 'primeflex/primeflex.css';

import React from 'react';
import { Routes, Route, BrowserRouter, HashRouter } from 'react-router-dom';
import './App.css';
import MainNavbar from './components/MainNavbar';
import HomePage from './pages/HomePage';
import MainFooter from './components/MainFooter';
import ShowDetails from './pages/ShowDetails';
import Login from './pages/UserService/Login';
import { UserContextProvider } from './context/UserContext';
import Payment from './pages/Payment';
import Shows from './pages/Shows'
import Events from './pages/Events'
import ShowsByTag from './pages/ShowsByTag';
import Register from './pages/UserService/Register';
import CreateEvent from './pages/OrganizatorPages/CreateEvent';
import CreateShow from './pages/OrganizatorPages/CreateShow';
import CreatedEvents from './pages/OrganizatorPages/CreatedEvents';
import CreatedShows from './pages/OrganizatorPages/CreatedShows';
import EditEvent from './pages/OrganizatorPages/EditEvent';
import EditShow from './pages/OrganizatorPages/EditShow';


function App() {
    return (
        <div>
            <HashRouter>
                <UserContextProvider>
                    <MainNavbar />
                    <Routes>    
                        <Route path="/" element={<HomePage />}/>
                        <Route path="/:slug/:showId/" element={<ShowDetails />}/>

                        <Route path="/shows/" element={<Shows />}/>
                        <Route path="/events/" element={<Events />}/>
                        <Route path="/tags/:tagName/" element={<ShowsByTag />}/>

                        <Route path="/create/event" element={<CreateEvent />} />
                        <Route path="/create/show" element={<CreateShow />} />
                        <Route path="/created/events" element={<CreatedEvents />} />
                        <Route path="/created/shows" element={<CreatedShows />} />
                        <Route path="/edit/:eventId" element={<EditEvent />} />
                        <Route path="/edit/:showId" element={<EditShow />} />

                        <Route path="/login/" element={<Login />}/>
                        <Route path="/register/" element={<Register />}/>

                        <Route path="/payment/" element={<Payment />}/>
                    </Routes>
                    <MainFooter />
                </UserContextProvider>
            </HashRouter>            
        </div>
    );
}

export default App;
