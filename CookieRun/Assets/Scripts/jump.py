import cv2
import mediapipe as mp
import time as time
import os

os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3'
mp_pose = mp.solutions.pose
mp_drawing = mp.solutions.drawing_utils

def detectPoseOnWebcam(pose):
    '''
    This function performs pose detection on the frames captured from webcam.
    Args:
        pose: The pose setup function required to perform the pose detection.
    Returns:
        None
    '''
    # 웹캠 열기
    cap = cv2.VideoCapture(0)
    prev_left_ankle_y = 0
    prev_right_ankle_y = 0
    test = 0

    while cap.isOpened():
        ret, frame = cap.read()
        if not ret:
            break

        frameRGB = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        results = pose.process(frameRGB)
        height, width, _ = frame.shape

        if results.pose_landmarks:
            mp_drawing.draw_landmarks(image=frame, landmark_list=results.pose_landmarks, connections=mp_pose.POSE_CONNECTIONS)
            curr_left_ankle_y = results.pose_landmarks.landmark[mp_pose.PoseLandmark.LEFT_ANKLE].y * height
            curr_right_ankle_y = results.pose_landmarks.landmark[mp_pose.PoseLandmark.RIGHT_ANKLE].y * height
            diff_left = curr_left_ankle_y - prev_left_ankle_y
            diff_right = curr_right_ankle_y - prev_right_ankle_y

            if diff_left > 25 or diff_right > 25:
                test = test + 1  
                print(test, flush=True)

            prev_left_ankle_y = curr_left_ankle_y
            prev_right_ankle_y = curr_right_ankle_y

        cv2.imshow('Webcam', frame)

        if cv2.waitKey(1) & 0xFF == ord('q'):
            break

    cap.release()
    cv2.destroyAllWindows()

pose = mp_pose.Pose(min_detection_confidence=0.5, min_tracking_confidence=0.5)
detectPoseOnWebcam(pose)
