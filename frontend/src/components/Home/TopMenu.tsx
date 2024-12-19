import React, { useState, useEffect } from 'react';
import {
  AppBar,
  Toolbar,
  Typography,
  Button,
  Box,
  IconButton,
  Drawer,
  List,
  ListItem,
  ListItemText,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { useNavigate } from 'react-router-dom';

function IconPersonHeart(props: React.SVGProps<SVGSVGElement>) {
  return (
    <svg
      fill="currentColor"
      viewBox="0 0 16 16"
      height="1em"
      width="1em"
      {...props}
    >
      <path d="M9 5a3 3 0 11-6 0 3 3 0 016 0zm-9 8c0 1 1 1 1 1h10s1 0 1-1-1-4-6-4-6 3-6 4zm13.5-8.09c1.387-1.425 4.855 1.07 0 4.277-4.854-3.207-1.387-5.702 0-4.276z" />
    </svg>
  );
}

const TopMenu: React.FC = () => {
  const navigate = useNavigate();
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const [isDrawerOpen, setIsDrawerOpen] = useState<boolean>(false); // State for hamburger menu

  // Function to check login status
  const checkAuthStatus = () => {
    const token = localStorage.getItem('token');
    setIsLoggedIn(!!token); // Set to true if token exists, false otherwise
  };

  useEffect(() => {
    // Check login status on component mount
    checkAuthStatus();

    // Listen for custom "auth-update" events
    const handleAuthUpdate = () => {
      checkAuthStatus();
    };

    window.addEventListener('auth-update', handleAuthUpdate);

    return () => {
      window.removeEventListener('auth-update', handleAuthUpdate);
    };
  }, []);

  const handleNavigation = (path: string) => {
    navigate(path);
    setIsDrawerOpen(false); // Close the drawer when navigating
  };

  const handleLogout = () => {
    // Clear authentication token or any stored user data
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    window.dispatchEvent(new Event('auth-update')); // Trigger custom event
    setIsLoggedIn(false); // Update state
    navigate('/'); // Redirect to home page
    setIsDrawerOpen(false); // Close the drawer
  };

  const handleLogin = () => {
    navigate('/login'); // Redirect to login page
    setIsDrawerOpen(false); // Close the drawer
  };

  const handleLogoClick = () => {
    navigate('/'); // Redirect to home page
  };

  return (
    <AppBar position="static">
      <Toolbar>
        {/* App Icon */}
        <Box sx={{ display: 'flex', alignItems: 'center', marginRight: 2 }}>
          <IconPersonHeart onClick={handleLogoClick} />
        </Box>

        {/* Desktop Navigation */}
        <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
          <Button
            color="inherit"
            onClick={() => handleNavigation('/users')}
            sx={{ fontFamily: 'cursive' }}
          >
            Users
          </Button>
          <Button color="inherit" onClick={() => handleNavigation('/influencers')}>
            Influencers
          </Button>
          <Button color="inherit" onClick={() => handleNavigation('/companies')}>
            Companies
          </Button>
          {isLoggedIn ? (
            <Button color="inherit" onClick={handleLogout}>
              Logout
            </Button>
          ) : (
            <Button color="inherit" onClick={handleLogin}>
              Login
            </Button>
          )}
        </Box>

        {/* Mobile Hamburger Menu */}
        <IconButton
          color="inherit"
          edge="end"
          sx={{ display: { xs: 'block', md: 'none' } }}
          onClick={() => setIsDrawerOpen(true)}
        >
          <MenuIcon />
        </IconButton>

        {/* Drawer for Mobile Navigation */}
        <Drawer anchor="right" open={isDrawerOpen} onClose={() => setIsDrawerOpen(false)}>
  <List>
    <ListItem component="button" onClick={() => handleNavigation('/users')}>
      <ListItemText primary="Users" sx={{ fontFamily: 'cursive' }} />
    </ListItem>
    <ListItem component="button" onClick={() => handleNavigation('/influencers')}>
      <ListItemText primary="Influencers" />
    </ListItem>
    <ListItem component="button" onClick={() => handleNavigation('/companies')}>
      <ListItemText primary="Companies" />
    </ListItem>
    {isLoggedIn ? (
      <ListItem component="button" onClick={handleLogout}>
        <ListItemText primary="Logout" />
      </ListItem>
    ) : (
      <ListItem component="button" onClick={handleLogin}>
        <ListItemText primary="Login" />
      </ListItem>
    )}
  </List>
</Drawer>
      </Toolbar>
    </AppBar>
  );
};

export default TopMenu;
