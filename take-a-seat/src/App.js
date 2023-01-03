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



                        <Route path="/login/" element={<Login />}/>
                        <Route path="/payment/" element={<Payment />}/>
                    </Routes>
                    <MainFooter />
                </UserContextProvider>
            </HashRouter>            
        </div>
    );
}

export default App;
