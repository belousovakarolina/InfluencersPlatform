import React, { useState } from 'react';
import { TextField, Button, Typography, CircularProgress } from '@mui/material';
import axios from '../../api/axiosConfig'; // Use centralized axios configuration
import { jwtDecode } from 'jwt-decode';
import { useNavigate } from 'react-router-dom'; // Import useNavigate
import { API_ENDPOINTS } from '../../config/apiConfig'; // Import API endpoints

interface JwtPayload {
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": string;
  jti: string;
  sub: string;
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string[];
  exp: number;
  iss: string;
  aud: string;
}
const Login: React.FC = () => {
  const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const navigate = useNavigate(); // Initialize useNavigate hook

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      const response = await axios.post('/user/login', {
        userName: username, // Ensure this matches the backend expectation
        password: password,
      });
      localStorage.setItem('token', response.data.accessToken); // Save token for authenticated requests
      // Decode the token
      const decoded = jwtDecode<JwtPayload>(response.data.accessToken);
      // Extract values
      const name = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
      const sub = decoded.sub;
      // Extract roles
      const roles = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      // Save roles to local storage as a JSON string
      if (roles && (Array.isArray(roles) || typeof(roles) === 'string')) {
        localStorage.setItem('roles', JSON.stringify(roles));
        console.log('Roles saved to localStorage:', roles);
      } else {
        console.warn('No roles found in the token.');
      }
        // Save to localStorage
        localStorage.setItem('username', name);
        localStorage.setItem('userId', sub);
      // Dispatch event to update state in other components
      window.dispatchEvent(new Event('auth-update'));

      // Redirect to the homepage
       navigate('/'); // Redirect to the home page
    } catch (err: any) {
      console.error('Backend error response:', err.response); // Log backend response for debugging
      setError(err.response?.data || 'Login failed. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} style={{ maxWidth: 400, margin: '0 auto' }}>
      <Typography variant="h4" gutterBottom>
        Login
      </Typography>
      <TextField
        label="Username"
        variant="outlined"
        fullWidth
        margin="normal"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
        required
      />
      <TextField
        label="Password"
        variant="outlined"
        fullWidth
        margin="normal"
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        required
      />
      {error && (
        <Typography color="error" variant="body2" gutterBottom>
          {error}
        </Typography>
      )}
      <Button
        type="submit"
        variant="contained"
        color="primary"
        fullWidth
        disabled={loading}
      >
        {loading ? <CircularProgress size={24} /> : 'Login'}
      </Button>
    </form>
  );
};

export default Login;
