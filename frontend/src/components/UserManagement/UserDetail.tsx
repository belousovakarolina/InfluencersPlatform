import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Card, CardContent, Typography, CircularProgress, Alert } from '@mui/material';
import axios from '../../api/axiosConfig';

interface User {
  name: string;
  email: string;
  phone: string;
  influencerProfileId?: number;
  companyProfileId?: number;
}

const UserDetail: React.FC = () => {
  const { id } = useParams();
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    axios
      .get(`/user/${id}`)
      .then((response) => {
        setUser(response.data);
      })
      .catch((err) => {
        setError('Failed to fetch user details.');
      })
      .finally(() => {
        setLoading(false);
      });
  }, [id]);

  if (loading) return <CircularProgress />;
  if (error) return <Alert severity="error">{error}</Alert>;

  return (
    <Card>
      <CardContent>
        <Typography variant="h5">User Details</Typography>
        <Typography><strong>Name:</strong> {user?.name || 'N/A'}</Typography>
        <Typography><strong>Email:</strong> {user?.email}</Typography>
        <Typography><strong>Phone:</strong> {user?.phone}</Typography>
        <Typography><strong>Influencer Profile ID:</strong> {user?.influencerProfileId ?? 'N/A'}</Typography>
        <Typography><strong>Company Profile ID:</strong> {user?.companyProfileId ?? 'N/A'}</Typography>
      </CardContent>
    </Card>
  );
};

export default UserDetail;
