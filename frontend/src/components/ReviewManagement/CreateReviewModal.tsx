import React, { useState } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Button,
  Typography,
  Box,
  Alert,
  CircularProgress,
} from '@mui/material';
import axios from '../../api/axiosConfig'; // Import configured Axios instance

interface CreateReviewModalProps {
  open: boolean;
  onClose: () => void;
  influencerId: string; // Passed from parent
}

const CreateReviewModal: React.FC<CreateReviewModalProps> = ({
  open,
  onClose,
  influencerId,
}) => {
  const [formData, setFormData] = useState({
    influencerId: influencerId,
    companyId: localStorage.getItem('userId') || '', // Set to logged-in user's ID
    userId: localStorage.getItem('userId') || '', // Set to logged-in user's ID
    name: '',
    description: '',
    stars: 3,
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const handleInputChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: name === 'stars' ? parseInt(value) : value,
    });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    setSuccess(null);

    try {
      await axios.post('/review', formData); // Replace with your API endpoint
      setSuccess('Review created successfully!');
      setTimeout(() => {
        onClose(); // Close modal after success
      }, 2000);
    } catch (err: any) {
      console.error('Error creating review:', err);
      setError(err.response?.data || 'Failed to create review.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>Create Review</DialogTitle>
      <DialogContent>
        {success && <Alert severity="success">{success}</Alert>}
        {error && <Alert severity="error">{error}</Alert>}
        <Box component="form" onSubmit={handleSubmit} noValidate>
          <TextField
            label="Reviewer Name"
            name="name"
            fullWidth
            margin="normal"
            value={formData.name}
            onChange={handleInputChange}
            required
          />
          <TextField
            label="Description"
            name="description"
            fullWidth
            margin="normal"
            multiline
            rows={4}
            value={formData.description}
            onChange={handleInputChange}
            required
          />
          <TextField
            label="Stars (1-5)"
            name="stars"
            type="number"
            fullWidth
            margin="normal"
            value={formData.stars}
            onChange={handleInputChange}
            inputProps={{ min: 1, max: 5 }}
            required
          />
        </Box>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} color="secondary" disabled={loading}>
          Cancel
        </Button>
        <Button
          onClick={handleSubmit}
          color="primary"
          variant="contained"
          disabled={loading}
        >
          {loading ? <CircularProgress size={24} /> : 'Submit Review'}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default CreateReviewModal;
