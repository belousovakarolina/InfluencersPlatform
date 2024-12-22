import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const HomePage: React.FC = () => {
  const navigate = useNavigate();

  useEffect(() => {
    // Check if the user is logged in by verifying the token
    const token = localStorage.getItem('token');

    if (token) {
      // If the user is logged in, redirect to the Influencers page
      navigate('/influencers');
    } else {
      // If not logged in, redirect to the Login page
      navigate('/login');
    }
  }, [navigate]);

  // Optional: Render a loading placeholder while redirecting
  return <div>Loading...</div>;
};

export default HomePage;
