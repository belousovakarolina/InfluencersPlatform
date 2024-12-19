import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import CreateReviewModal from '../ReviewManagement/CreateReviewModal'; // Import the modal component
import {
  Typography,
  Card,
  CardContent,
  CircularProgress,
  Alert,
  List,
  ListItem,
  ListItemText,
  Divider,
  Button,
  Box,
} from '@mui/material';
import axios from '../../api/axiosConfig'; // Import the configured Axios instance
import influencerPhoto from '../../assets/influencer_photo.jpg'

// Define the Influencer interface based on the API response
interface Influencer {
  id: number;
  userId: string; // Use this for review retrieval
  categoryId?: number | null;
  name: string;
  description?: string | null;
  igFollowerCount?: number | null;
  fbFollowerCount?: number | null;
  tiktokFollowerCount?: number | null;
}

// Define the Review interface
interface Review {
  id: number;
  influencerId?: string | null;
  companyId?: string | null;
  userId: string;
  name?: string | null;
  description: string;
  stars?: number | null;
  verified: boolean;
  createdDate: string;
}

const InfluencerDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>(); // Get ID from route params
  const [influencer, setInfluencer] = useState<Influencer | null>(null);
  const [reviews, setReviews] = useState<Review[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [reviewLoading, setReviewLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [reviewError, setReviewError] = useState<string | null>(null);
  const [isReviewModalOpen, setIsReviewModalOpen] = useState(false); // Modal open state

  useEffect(() => {
    // Fetch the influencer data by ID
    axios
      .get(`/influencerprofile/${id}`)
      .then((response) => {
        setInfluencer(response.data);

        // Fetch reviews using userId after influencer data is loaded
        axios
          .get(`/review/influencer/${response.data.userId}`)
          .then((reviewResponse) => {
            setReviews(reviewResponse.data);
            setReviewLoading(false);
          })
          .catch((err) => {
            if (err.response?.status === 404) {
              // Handle no reviews case
              setReviewError('No reviews about this influencer.');
            } else {
              setReviewError('Failed to fetch reviews.');
            }
            setReviewLoading(false);
          });

        setLoading(false);
      })
      .catch((err) => {
        console.error('Error fetching influencer details:', err);
        setError('Failed to fetch influencer details.');
        setLoading(false);
      });
  }, [id]);

  if (loading) {
    return <CircularProgress />;
  }

  if (error) {
    return <Alert severity="error">{error}</Alert>;
  }

  if (!influencer) {
    return <Alert severity="info">No influencer found with this ID.</Alert>;
  }

  return (
    <Card>
      <CardContent>
        <Typography variant="h5" gutterBottom>
          Influencer Details
        </Typography>
        {/* Static Picture */}
        <Box
          component="img"
          src= { influencerPhoto }
          alt="Influencer"
          sx={{
            maxWidth: '300px', // Maximum width of 300px
            width: '100%', // Make it responsive
            height: 'auto', // Maintain aspect ratio
            marginBottom: 2,
            display: 'block',
            marginLeft: 'auto',
            marginRight: 'auto', // Center the image
          }}
        />
        <Typography variant="body1">
          <strong>ID:</strong> {influencer.id}
        </Typography>
        <Typography variant="body1">
          <strong>User ID:</strong> {influencer.userId}
        </Typography>
        <Typography variant="body1">
          <strong>Category ID:</strong> {influencer.categoryId ?? 'N/A'}
        </Typography>
        <Typography variant="body1">
          <strong>Name:</strong> {influencer.name}
        </Typography>
        <Typography variant="body1">
          <strong>Description:</strong> {influencer.description || 'N/A'}
        </Typography>
        <Typography variant="body1">
          <strong>Instagram Followers:</strong> {influencer.igFollowerCount ?? 'N/A'}
        </Typography>
        <Typography variant="body1">
          <strong>Facebook Followers:</strong> {influencer.fbFollowerCount ?? 'N/A'}
        </Typography>
        <Typography variant="body1">
          <strong>TikTok Followers:</strong> {influencer.tiktokFollowerCount ?? 'N/A'}
        </Typography>
      </CardContent>

      {/* Button to Open Review Modal */}
      <CardContent>
        <Button
          variant="contained"
          color="primary"
          onClick={() => setIsReviewModalOpen(true)}
        >
          Leave a Review
        </Button>
      </CardContent>

      {/* Reviews Section */}
      <CardContent>
        <Typography variant="h5" gutterBottom>
          Reviews
        </Typography>
        {reviewLoading ? (
          <CircularProgress />
        ) : reviewError ? (
          <Alert severity="info">{reviewError}</Alert>
        ) : (
          <List>
            {reviews.map((review) => (
              <React.Fragment key={review.id}>
                <ListItem>
                  <ListItemText
                    primary={
                      <>
                        <Typography variant="body2">
                          <strong>Reviewer Name:</strong> {review.name || 'Anonymous'}
                        </Typography>
                        <Typography variant="body2">
                          <strong>Stars:</strong> {review.stars ?? 'N/A'}
                        </Typography>
                      </>
                    }
                    secondary={
                      <>
                        <Typography variant="body2">
                          <strong>Verified:</strong> {review.verified ? 'Yes' : 'No'}
                        </Typography>
                        <Typography variant="body2">
                          <strong>Description:</strong> {review.description}
                        </Typography>
                        <Typography variant="body2">
                          <strong>Date:</strong> {new Date(review.createdDate).toLocaleDateString()}
                        </Typography>
                      </>
                    }
                  />
                </ListItem>
                <Divider />
              </React.Fragment>
            ))}
          </List>
        )}
      </CardContent>

      {/* Create Review Modal */}
      <CreateReviewModal
        open={isReviewModalOpen}
        onClose={() => setIsReviewModalOpen(false)}
        influencerId={influencer.userId} // Pass Influencer's userId
      />
    </Card>
  );
};

export default InfluencerDetail;
