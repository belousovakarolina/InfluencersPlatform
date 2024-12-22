import React, { useState } from 'react';
import { TextField, Button, Typography, Box, Alert, CircularProgress, Container } from '@mui/material';
import axios from '../../api/axiosConfig'; // Import the configured Axios instance
import { useNavigate } from 'react-router-dom';

const RegisterInfluencer: React.FC = () => {
  const [formData, setFormData] = useState({
    userName: '',
    password: '',
    email: '',
  });
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    setSuccess(null);

    // Send data to the registration API
    axios
      .post('/user/influencer', formData)
      .then(() => {
        setSuccess('Registration successful! Redirecting to login page...');
        setLoading(false);
        setTimeout(() => navigate('/login'), 2000); // Redirect to login after success
      })
      .catch((err) => {
        console.error('Error during registration:', err);
        setError(err.response?.data || 'Registration failed. Please try again.');
        setLoading(false);
      });
  };

  return (
    <Container maxWidth="sm">
      <Typography variant="h5" gutterBottom>
        Register as Influencer
      </Typography>
      {success && <Alert severity="success">{success}</Alert>}
      {error && <Alert severity="error">{error}</Alert>}
      <form onSubmit={handleSubmit}>
        <TextField
          label="Username"
          name="userName"
          fullWidth
          margin="normal"
          required
          value={formData.userName}
          onChange={handleInputChange}
        />
        <TextField
          label="Email"
          name="email"
          type="email"
          fullWidth
          margin="normal"
          required
          value={formData.email}
          onChange={handleInputChange}
        />
        <TextField
          label="Password"
          name="password"
          type="password"
          fullWidth
          margin="normal"
          required
          value={formData.password}
          onChange={handleInputChange}
          />
        <Box mt={2}>
          <Button
            type="submit"
            variant="contained"
            color="primary"
            fullWidth
            disabled={loading}
          >
            {loading ? <CircularProgress size={24} /> : 'Register'}
          </Button>
        </Box>
      </form>
    </Container>
  );
};

export default RegisterInfluencer;
