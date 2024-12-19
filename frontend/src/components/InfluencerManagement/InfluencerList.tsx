import React, { useState, useEffect } from 'react';
import {
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  CircularProgress,
  Alert,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  Box,
  Grid,
  useMediaQuery,
} from '@mui/material';
import axios from '../../api/axiosConfig'; // Import the configured Axios instance
import { useNavigate } from 'react-router-dom';

// Define the Influencer interface based on the API response
interface Influencer {
  id: number;
  userId: string;
  categoryId?: number | null;
  name: string;
  description?: string | null;
  igFollowerCount?: number | null;
  fbFollowerCount?: number | null;
  tiktokFollowerCount?: number | null;
}

const InfluencerList: React.FC = () => {
  const [influencers, setInfluencers] = useState<Influencer[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [selectedInfluencer, setSelectedInfluencer] = useState<Influencer | null>(null);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState<boolean>(false);
  const navigate = useNavigate();
  const isMobile = useMediaQuery('(max-width:768px)'); // Check if screen width is <= 768px

  // Retrieve roles from localStorage
  const roles: string[] = JSON.parse(localStorage.getItem('roles') || '[]') as string[];

  // Check if user has Administrator or Company role
  const canCreateInfluencer = roles.includes('Influencer') || typeof roles === 'string';
  console.log(typeof roles)

  useEffect(() => {
    // Fetch the influencer data from the API
    axios
      .get('/influencerprofile')
      .then((response) => {
        setInfluencers(response.data);
        setLoading(false);
      })
      .catch((err) => {
        console.error('Error fetching influencer profiles:', err);
        setError('Failed to fetch influencer profiles.');
        setLoading(false);
      });
  }, []);

  const handleOpenDeleteModal = (influencer: Influencer) => {
    setSelectedInfluencer(influencer);
    setIsDeleteModalOpen(true);
  };

  const handleCloseDeleteModal = () => {
    setSelectedInfluencer(null);
    setIsDeleteModalOpen(false);
  };

  const handleDeleteInfluencer = () => {
    if (!selectedInfluencer) return;

    setLoading(true);
    axios
      .delete(`/influencerprofile/${selectedInfluencer.id}`)
      .then(() => {
        setInfluencers((prev) =>
          prev.filter((influencer) => influencer.id !== selectedInfluencer.id)
        );
        setIsDeleteModalOpen(false);
        setLoading(false);
      })
      .catch((err) => {
        console.error('Error deleting influencer:', err);
        setError('Failed to delete influencer.');
        setLoading(false);
      });
  };

  if (loading && !isDeleteModalOpen) {
    return <CircularProgress />;
  }

  if (error) {
    return <Alert severity="error">{error}</Alert>;
  }

  return (
    <>
      <TableContainer component={Paper}>
        <Typography variant="h6" gutterBottom>
          Influencer List
        </Typography>

        {/* Conditionally Render Create Influencer Button */}
        {canCreateInfluencer && (
          <Button
            variant="contained"
            color="primary"
            onClick={() => navigate('/influencers/create')}
            style={{ marginBottom: '1rem' }}
          >
            Create Influencer Profile
          </Button>
        )}

        {isMobile ? (
          <Grid container spacing={2}>
            {influencers.map((influencer) => (
              <Grid item xs={12} key={influencer.id}>
                <Box
                  border={1}
                  borderColor="grey.300"
                  borderRadius={2}
                  padding={2}
                  marginBottom={2}
                >
                  <Typography variant="body1">
                    <strong>ID:</strong> {influencer.id}
                  </Typography>
                  <Typography variant="body1">
                    <strong>User ID:</strong> {influencer.userId}
                  </Typography>
                  <Typography variant="body1">
                    <strong>Name:</strong> {influencer.name}
                  </Typography>
                  <Typography variant="body1">
                    <strong>Description:</strong> {influencer.description || 'N/A'}
                  </Typography>
                  <Button
                    variant="contained"
                    color="primary"
                    onClick={() => navigate(`/influencer/edit/${influencer.id}`)}
                    size="small"
                  >
                    Edit
                  </Button>
                  <Button
                    variant="outlined"
                    onClick={() => navigate(`/influencer/${influencer.id}`)}
                    size="small"
                    style={{ marginLeft: '0.5rem' }}
                  >
                    View Details
                  </Button>
                  <Button
                    variant="contained"
                    color="secondary"
                    onClick={() => handleOpenDeleteModal(influencer)}
                    size="small"
                    style={{ marginLeft: '0.5rem' }}
                  >
                    Delete
                  </Button>
                </Box>
              </Grid>
            ))}
          </Grid>
        ) : (
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Id</TableCell>
                <TableCell>User Id</TableCell>
                <TableCell>Category Id</TableCell>
                <TableCell>Name</TableCell>
                <TableCell>Description</TableCell>
                <TableCell>Instagram Followers</TableCell>
                <TableCell>Facebook Followers</TableCell>
                <TableCell>TikTok Followers</TableCell>
                <TableCell>Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {influencers.map((influencer) => (
                <TableRow key={influencer.id}>
                  <TableCell>{influencer.id}</TableCell>
                  <TableCell>{influencer.userId}</TableCell>
                  <TableCell>{influencer.categoryId ?? 'N/A'}</TableCell>
                  <TableCell>{influencer.name}</TableCell>
                  <TableCell>{influencer.description || 'N/A'}</TableCell>
                  <TableCell>{influencer.igFollowerCount ?? 'N/A'}</TableCell>
                  <TableCell>{influencer.fbFollowerCount ?? 'N/A'}</TableCell>
                  <TableCell>{influencer.tiktokFollowerCount ?? 'N/A'}</TableCell>
                  <TableCell>
                    <Button
                      variant="contained"
                      color="primary"
                      onClick={() => navigate(`/influencer/edit/${influencer.id}`)}
                    >
                      Edit
                    </Button>
                    <Button
                      variant="outlined"
                      onClick={() => navigate(`/influencer/${influencer.id}`)}
                    >
                      View Details
                    </Button>
                    <Button
                      variant="contained"
                      color="secondary"
                      onClick={() => handleOpenDeleteModal(influencer)}
                      style={{ marginLeft: '0.5rem' }}
                    >
                      Delete
                    </Button>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        )}
      </TableContainer>

      {/* Delete Confirmation Modal */}
      <Dialog
        open={isDeleteModalOpen}
        onClose={handleCloseDeleteModal}
        aria-labelledby="delete-dialog-title"
        aria-describedby="delete-dialog-description"
      >
        <DialogTitle id="delete-dialog-title">Confirm Delete</DialogTitle>
        <DialogContent>
          <DialogContentText id="delete-dialog-description">
            Are you sure you want to delete{' '}
            <strong>{selectedInfluencer?.name}</strong>'s profile?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseDeleteModal} color="primary">
            Cancel
          </Button>
          <Button onClick={handleDeleteInfluencer} color="secondary" autoFocus>
            Delete
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
};

export default InfluencerList;
