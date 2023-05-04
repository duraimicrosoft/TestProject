@ECHO OFF
SET PATH=%PATH%;F:\GnuPG
gpg --yes --batch --passphrase-fd 0  --output "@destinationFile" --decrypt "@sourceFile"