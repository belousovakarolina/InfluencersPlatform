// src/components/Auth/ProtectedRoute.tsx
import React from 'react';
import { Navigate } from 'react-router-dom';

interface ProtectedRouteProps {
  component: JSX.Element;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ component }) => {
  const token = localStorage.getItem('token');

  return token ? component : <Navigate to="/login" />;
};

export default ProtectedRoute;
