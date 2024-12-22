import React, { useState, useEffect } from 'react';
import { AppBar, Toolbar, Typography, Box, Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const BottomMenu: React.FC = () => {
  const [username, setUsername] = useState<string | null>(null);
  const navigate = useNavigate();

  // Function to retrieve the username from localStorage
  const fetchUsername = () => {
    const token = localStorage.getItem('token');
    const storedUsername = localStorage.getItem('username');
    setUsername(token && storedUsername ? storedUsername : null);
  };

  // Update username on component mount and listen for changes to localStorage
  useEffect(() => {
    fetchUsername();

    // Listen for localStorage changes (e.g., login/logout in another tab or window)
    const handleAuthUpdate = () => {
      fetchUsername();
    };

    window.addEventListener('auth-update', handleAuthUpdate);

    // Cleanup listener when the component unmounts
    return () => {
      window.removeEventListener('auth-update', handleAuthUpdate);
    };
  }, []);

  return (
    <AppBar position="fixed" color="primary" sx={{ top: 'auto', bottom: 0 }}>
      <Toolbar>
        <Box sx={{ flexGrow: 1 }}>
          <Typography variant="body1" color="inherit">
            {username ? `Logged in as: ${username}` : 'Not logged in'}
          </Typography>
        </Box>
        {/* Conditionally render the Register button if the user is not logged in */}
        {!username && (
          <Button onClick={() => navigate('/register-options')} color="inherit">
            Register
          </Button>
        )}
      </Toolbar>
    </AppBar>
  );
};

export default BottomMenu;
