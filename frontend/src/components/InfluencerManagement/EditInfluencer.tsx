import React, { useEffect, useState } from 'react';
import {
  TextField,
  Button,
  Typography,
  CircularProgress,
  Alert,
  Box,
  Container,
} from '@mui/material';
import { useParams, useNavigate } from 'react-router-dom';
import axios from '../../api/axiosConfig'; // Import the configured Axios instance

// Define the Influencer interface
interface Influencer {
  categoryId?: number | null;
  name: string;
  description?: string | null;
  igFollowerCount?: number | null;
  fbFollowerCount?: number | null;
  tiktokFollowerCount?: number | null;
}

const EditInfluencer: React.FC = () => {
  const { id } = useParams<{ id: string }>(); // Get the influencer ID from the route
  const [influencer, setInfluencer] = useState<Influencer | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    // Fetch the current influencer data
    axios
      .get(`/influencerprofile/${id}`)
      .then((response) => {
        setInfluencer(response.data);
        setLoading(false);
      })
      .catch((err) => {
        console.error('Error fetching influencer details:', err);
        setError('Failed to fetch influencer details.');
        setLoading(false);
      });
  }, [id]);

  const handleInputChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    if (!influencer) return;
    const { name, value } = e.target;
    setInfluencer({ ...influencer, [name]: value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    setSuccess(null);

    // Update influencer data
    axios
      .put(`/influencerprofile/${id}`, influencer)
      .then(() => {
        setSuccess('Influencer updated successfully!');
        setLoading(false);
        setTimeout(() => navigate('/influencers'), 2000); // Redirect after success
      })
      .catch((err) => {
        console.error('Error updating influencer:', err);
        setError('Failed to update influencer. Please try again.');
        setLoading(false);
      });
  };

  if (loading && !influencer) {
    return <CircularProgress />;
  }

  if (error) {
    return <Alert severity="error">{error}</Alert>;
  }

  return (
    <Container maxWidth="sm">
      <Typography variant="h5" gutterBottom>
        Edit Influencer
      </Typography>
      {success && <Alert severity="success">{success}</Alert>}
      {error && <Alert severity="error">{error}</Alert>}
      {influencer && (
        <form onSubmit={handleSubmit}>
          <TextField
            label="Category ID"
            name="categoryId"
            type="number"
            fullWidth
            margin="normal"
            value={influencer.categoryId ?? ''}
            onChange={handleInputChange}
          />
          <TextField
            label="Name"
            name="name"
            fullWidth
            margin="normal"
            required
            value={influencer.name}
            onChange={handleInputChange}
          />
          <TextField
            label="Description"
            name="description"
            fullWidth
            margin="normal"
            value={influencer.description ?? ''}
            onChange={handleInputChange}
          />
          <TextField
            label="Instagram Followers"
            name="igFollowerCount"
            type="number"
            fullWidth
            margin="normal"
            value={influencer.igFollowerCount ?? ''}
            onChange={handleInputChange}
          />
          <TextField
            label="Facebook Followers"
            name="fbFollowerCount"
            type="number"
            fullWidth
            margin="normal"
            value={influencer.fbFollowerCount ?? ''}
            onChange={handleInputChange}
          />
          <TextField
            label="TikTok Followers"
            name="tiktokFollowerCount"
            type="number"
            fullWidth
            margin="normal"
            value={influencer.tiktokFollowerCount ?? ''}
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
              {loading ? <CircularProgress size={24} /> : 'Save Changes'}
            </Button>
          </Box>
        </form>
      )}
    </Container>
  );
};

export default EditInfluencer;
