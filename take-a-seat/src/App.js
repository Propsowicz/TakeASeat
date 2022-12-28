/* eslint-disable */

import 'primereact/resources/themes/arya-green/theme.css';
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


function App() {
    return (
        <div>
            <HashRouter>
                <UserContextProvider>
                    <MainNavbar />
                    <Routes>    
                        <Route path="/" element={<HomePage />}/>
                        <Route path="/:slug/:showId/" element={<ShowDetails />}/>

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
