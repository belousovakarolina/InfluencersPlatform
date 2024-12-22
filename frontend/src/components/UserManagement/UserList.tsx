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
  Button,
} from '@mui/material';
import { useNavigate } from 'react-router-dom';
import axios from '../../api/axiosConfig'; // Import the configured axios instance
import { API_ENDPOINTS } from '../../config/apiConfig'; // Import API endpoints

// Define the User interface to match the API response
interface User {
  id: string;
  email: string;
  roles: string;
  influencerProfileId?: number;
  companyProfileId?: number;
}

const UserList: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const navigate = useNavigate(); // React Router's hook for navigation

  useEffect(() => {
    // Fetch user data from the API
    axios
      .get(API_ENDPOINTS.USER_LIST)
      .then((response) => {
        setUsers(response.data);
        setLoading(false);
      })
      .catch((err) => {
        console.error('Error fetching users:', err);
        setError('Failed to fetch users.');
        setLoading(false);
      });
  }, []);

  const handleViewUser = (userId: string) => {
    navigate(`/user/${userId}`); // Navigate to the user detail page
  };

  if (loading) {
    return <CircularProgress />;
  }

  if (error) {
    return <Typography color="error">{error}</Typography>;
  }

  return (
    <TableContainer component={Paper}>
      <Typography variant="h6" gutterBottom>
        User List
      </Typography>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>ID</TableCell>
            <TableCell>Email</TableCell>
            <TableCell>Roles</TableCell>
            <TableCell>Influencer Profile ID</TableCell>
            <TableCell>Company Profile ID</TableCell>
            <TableCell>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {users.map((user) => (
            <TableRow key={user.id}>
              <TableCell>{user.id}</TableCell>
              <TableCell>{user.email}</TableCell>
              <TableCell>{user.roles}</TableCell>
              <TableCell>{user.influencerProfileId || '-'}</TableCell>
              <TableCell>{user.companyProfileId || '-'}</TableCell>
              <TableCell>
                <Button
                  variant="contained"
                  color="primary"
                  size="small"
                  onClick={() => handleViewUser(user.id)}
                >
                  View User
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default UserList;
