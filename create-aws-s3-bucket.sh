#!/bin/bash
#
#This script will create the AWS S3 bucket
#
#Run this script on your AWS CloudShell console or run it with a user with the required privileges
#
#run script 
#./create-aws-s3-bucket.sh
#
#This script will:
# - Create S3 Bucket named ipgeoguardrepo => must be lowercase, aws restriction

echo Create IPGeoGuardRepo
aws s3api create-bucket --bucket ipgeoguardrepo --region sa-east-1 --create-bucket-configuration LocationConstraint=sa-east-1
