#!/bin/bash
#
#This script will create the AWS User credentials needed to access AWS Lambda resources
#
#Run this script on your AWS CloudShell console or run it with a user with the required privileges
#
#run script 
#./create-aws-lambda-user-roles.sh
#
#This script will:
# - Create Group named LambdaGroup
# - Create User named LambdaUser adding it to LambdaGroup
# - Apply FullAccessLambda and ECR_AllowAuthorization roles to LambdaGroup
# - Create Role IPGeoGuardRole
# - Apply AWSLambdaBasicExecutionRole role to IPGeoGuardRole

echo Create LambdaGroup
aws iam create-group --group-name LambdaGroup

echo Create LambdaUser
aws iam create-user --user-name LambdaUser

echo Add LambdaUser to LambdaGroup
aws iam add-user-to-group --user-name LambdaUser --group-name LambdaGroup

echo Apply FullAccessLambda to LambdaGroup
aws iam attach-group-policy --group-name LambdaGroup --policy-arn arn:aws:iam::aws:policy/AWSLambda_FullAccess

echo Apply AmazonS3FullAccess to LambdaGroup
aws iam attach-group-policy --group-name LambdaGroup --policy-arn arn:aws:iam::aws:policy/AmazonS3FullAccess

echo Apply ECR_AllowAuthorization to LambdaGroup
aws iam attach-group-policy --group-name LambdaGroup --policy-arn arn:aws:iam::091201685298:policy/ECR_AllowAuthorization

echo Create IPGeoGuardRole
aws iam create-role --role-name IPGeoGuardRole --assume-role-policy-document '{"Version": "2012-10-17","Statement": [{ "Effect": "Allow", "Principal": {"Service": "lambda.amazonaws.com"}, "Action": "sts:AssumeRole"}]}'

echo Apply AWSLambdaBasicExecutionRole to IPGeoGuardRole
aws iam attach-role-policy --role-name IPGeoGuardRole --policy-arn arn:aws:iam::aws:policy/service-role/AWSLambdaBasicExecutionRole
aws iam attach-role-policy --role-name IPGeoGuardRole --policy-arn arn:aws:iam::aws:policy/AmazonS3FullAccess

echo Create Access Key
aws iam create-access-key --user-name LambdaUser

echo "You can use the LambdaUser credentials AccessKeyId and SecretAccessKey to consume AWS Lambda Function resources."

echo -e "\n~~ END ~~\n"
