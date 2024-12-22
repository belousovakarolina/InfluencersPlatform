import React, { useState } from 'react';
import {
  TextField,
  Button,
  Typography,
  Box,
  Container,
  CircularProgress,
  Alert,
} from '@mui/material';
import axios from '../../api/axiosConfig'; // Import the configured Axios instance
import { useNavigate } from 'react-router-dom';

// Define the Influencer interface for form data
interface Influencer {
  categoryId?: number | null;
  name: string;
  description?: string | null;
  igFollowerCount?: number | null;
  fbFollowerCount?: number | null;
  tiktokFollowerCount?: number | null;
}

const CreateInfluencer: React.FC = () => {
  const [formData, setFormData] = useState<Influencer>({
    categoryId: null,
    name: '',
    description: '',
    igFollowerCount: null,
    fbFollowerCount: null,
    tiktokFollowerCount: null,
  });

  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const navigate = useNavigate();

  const handleInputChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: name.includes('Count') || name === 'categoryId' ? Number(value) : value,
    });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    setSuccess(null);

    // Submit the form data to the API
    axios
      .post('/influencerprofile', formData)
      .then(() => {
        setSuccess('Influencer created successfully!');
        setLoading(false);
        setTimeout(() => navigate('/influencers'), 2000); // Redirect to Influencer List page
      })
      .catch((err) => {
        console.error('Error creating influencer:', err);
        setError('Failed to create influencer. Please try again.');
        setLoading(false);
      });
  };

  return (
    <Container maxWidth="sm">
      <Typography variant="h5" gutterBottom>
        Create Influencer
      </Typography>
      {success && <Alert severity="success">{success}</Alert>}
      {error && <Alert severity="error">{error}</Alert>}
      <form onSubmit={handleSubmit}>
        <TextField
          label="Category ID"
          name="categoryId"
          type="number"
          fullWidth
          margin="normal"
          value={formData.categoryId ?? ''}
          onChange={handleInputChange}
        />
        <TextField
          label="Name"
          name="name"
          fullWidth
          margin="normal"
          required
          value={formData.name}
          onChange={handleInputChange}
        />
        <TextField
          label="Description"
          name="description"
          fullWidth
          margin="normal"
          value={formData.description ?? ''}
          onChange={handleInputChange}
        />
        <TextField
          label="Instagram Followers"
          name="igFollowerCount"
          type="number"
          fullWidth
          margin="normal"
          value={formData.igFollowerCount ?? ''}
          onChange={handleInputChange}
        />
        <TextField
          label="Facebook Followers"
          name="fbFollowerCount"
          type="number"
          fullWidth
          margin="normal"
          value={formData.fbFollowerCount ?? ''}
          onChange={handleInputChange}
        />
        <TextField
          label="TikTok Followers"
          name="tiktokFollowerCount"
          type="number"
          fullWidth
          margin="normal"
          value={formData.tiktokFollowerCount ?? ''}
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
            {loading ? <CircularProgress size={24} /> : 'Create Influencer'}
          </Button>
        </Box>
      </form>
    </Container>
  );
};

export default CreateInfluencer;
