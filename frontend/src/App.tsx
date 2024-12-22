// src/App.tsx
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import ProtectedRoute from './components/Auth/ProtectedRoute';
import Login from './components/Auth/Login';
import RegisterInfluencer from './components/Auth/RegisterInfluencer';
import RegisterCompany from './components/Auth/RegisterCompany';
import RegistrationOptions from './components/Auth/RegistrationOptions';
import UserList from './components/UserManagement/UserList';
import UserDetail from './components/UserManagement/UserDetail';
import InfluencerList from './components/InfluencerManagement/InfluencerList';
import InfluencerDetail from './components/InfluencerManagement/InfluencerDetail';
import EditInfluencer from './components/InfluencerManagement/EditInfluencer';
import CreateInfluencer from './components/InfluencerManagement/CreateInfluencer';
import CompanyList from './components/CompanyManagement/CompanyList';
import HomePage from './components/Home/HomePage';
import TopMenu from './components/Home/TopMenu';
import BottomMenu from './components/Home/BottomMenu';

const App: React.FC = () => {
  return (
    <Router>
      <TopMenu />
      <Routes>
        <Route path="/" element={<HomePage />} /> 
        <Route path="/login" element={<Login />} />
        <Route path="/register-influencer" element={<RegisterInfluencer />} />
        <Route path="/register-company" element={<RegisterCompany />} />
        <Route path="/register-options" element={<RegistrationOptions />} />
        <Route path="/users" element={<ProtectedRoute component={<UserList />} />} />
        <Route path="/user/:id" element={<ProtectedRoute component={<UserDetail />} />} />
        <Route path="/influencers" element={<InfluencerList />} />
        <Route path="/influencer/:id" element={<InfluencerDetail />} />
        <Route path="/influencer/edit/:id" element={<EditInfluencer />} />
        <Route path="/influencers/create" element={<CreateInfluencer />} />
        <Route path="/companies" element={<CompanyList />} />
        </Routes>
        <BottomMenu />
    </Router>
  );
};

export default App;
