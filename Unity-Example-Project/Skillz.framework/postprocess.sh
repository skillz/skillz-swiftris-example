/usr/bin/perl -x "$0"
exit

#!perl start here 
use strict;

$ENV{PATH} = $ENV{PATH} . ':/usr/libexec';

my $BUILT_PRODUCTS_DIR = $ENV{'BUILT_PRODUCTS_DIR'};
my $INFOPLIST_PATH = $ENV{'INFOPLIST_PATH'};

# escape some troublesome path characters
$BUILT_PRODUCTS_DIR =~ s!'!\'!g;
$BUILT_PRODUCTS_DIR =~ s!"!\"!g;
$BUILT_PRODUCTS_DIR =~ s! !\ !g;

$INFOPLIST_PATH =~ s!'!\'!g;
$INFOPLIST_PATH =~ s!"!\"!g;
$INFOPLIST_PATH =~ s! !\ !g;

print "Running Skillz SDK post processing script" . "\n";

print "Built Products Path: " . $BUILT_PRODUCTS_DIR . "\n";
print "Info.plist Path: " . $INFOPLIST_PATH . "\n";

# Add Plist value to properly inform user of Location Request for iOS 8

my $locationInUse = `PlistBuddy -c \'Print NSLocationWhenInUseUsageDescription\' "$BUILT_PRODUCTS_DIR/$INFOPLIST_PATH"`;

if (!length($locationInUse)) {
   `PlistBuddy -c \'Add :NSLocationWhenInUseUsageDescription string \"Due to legal requirements we require your location in games that can be played for cash.\"\' "$BUILT_PRODUCTS_DIR/$INFOPLIST_PATH"`;
}

# Add Plist value to respect view controller status bar appearance
`PlistBuddy -c \'Delete :UIViewControllerBasedStatusBarAppearance\' "$BUILT_PRODUCTS_DIR/$INFOPLIST_PATH"`;
`PlistBuddy -c \'Add :UIViewControllerBasedStatusBarAppearance bool YES\' "$BUILT_PRODUCTS_DIR/$INFOPLIST_PATH"`;

# Add Plist value to require full screen
`PlistBuddy -c \'Delete :UIRequiresFullScreen\' "$BUILT_PRODUCTS_DIR/$INFOPLIST_PATH"`;
`PlistBuddy -c \'Add :UIRequiresFullScreen bool YES\' "$BUILT_PRODUCTS_DIR/$INFOPLIST_PATH"`;

# Add Custom URL Scheme unique to your game.
my $bundleId = `PlistBuddy -c \"Print CFBundleIdentifier\" "$BUILT_PRODUCTS_DIR/$INFOPLIST_PATH"`;
my $customURLScheme = "skillz.gamelinks." . $bundleId;
$customURLScheme =~ s/\R//g;
print "Custom url scheme: $customURLScheme";

my $bundleTypes = `PlistBuddy -c \'Print CFBundleURLTypes\' "$BUILT_PRODUCTS_DIR/$INFOPLIST_PATH"`;

if (!length($bundleTypes)) {
    print "CFBundleURLTypes does not yet exist, create it.\n";
    `PlistBuddy -c \'Add :CFBundleURLTypes array\' "${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}"`;
    `PlistBuddy -c \'Add :CFBundleURLTypes: dict\' "${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}"`;
    `PlistBuddy -c \"Add :CFBundleURLTypes:0:CFBundleURLName string ${customURLScheme}\" \"${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}\"`;
    `PlistBuddy -c \'Add :CFBundleURLTypes:0:CFBundleURLSchemes array\' "${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}"`;
    `PlistBuddy -c \"Add :CFBundleURLTypes:0:CFBundleURLSchemes: string "${customURLScheme}"\" \"${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}\"`;
} else {
    print "CFBundleURLTypes exists, check if we should add Skillz. \n";
	# Only add our custom scheme if it does not yet exist
	my $customScheme = `grep --quiet "$customURLScheme" "$BUILT_PRODUCTS_DIR/$INFOPLIST_PATH"`;

	if (!length($customScheme)) {
        print "Skillz URL scheme not yet set \n";
		my $temporaryPlistPath = `dirname "$BUILT_PRODUCTS_DIR/$INFOPLIST_PATH/urlScheme.plist"`;

		unlink "$temporaryPlistPath";

		my $fileContents = "
					<array>
						<dict>
							<key>CFBundleURLName</key>
                                <string>${customURLScheme}</string>
							<key>CFBundleURLSchemes</key>
							<array>
                                <string>${customURLScheme}</string>
							</array>
						</dict>
					</array>";

        open(my $fh, '>', "$temporaryPlistPath");
        print "$fh" . "$fileContents" . "\n";
        print $fh $fileContents;
        close $fh;

        `PlistBuddy -c \'Merge $temporaryPlistPath :CFBundleURLTypes:\' "$BUILT_PRODUCTS_DIR/$INFOPLIST_PATH"`;

        unlink "$temporaryPlistPath";
    } else {
        print 'Skillz URL scheme already set';
    }
}

# Set up localization.
my $localizations = `PlistBuddy -c \'Print CFBundleLocalizations:\' "$BUILT_PRODUCTS_DIR/$INFOPLIST_PATH"`;

if (!length($localizations)) {

    `PlistBuddy -c \'Add :CFBundleLocalizations array\' "${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}"`;
    `PlistBuddy -c \'Add :CFBundleLocalizations:0 string en\' "${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}"`;
    `PlistBuddy -c \'Add :CFBundleLocalizations:1 string de\' "${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}"`;
    `PlistBuddy -c \'Add :CFBundleLocalizations:2 string es\' "${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}"`;
    `PlistBuddy -c \'Add :CFBundleLocalizations:3 string fr\' "${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}"`;
    `PlistBuddy -c \'Add :CFBundleLocalizations:4 string it\' "${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}"`;
    `PlistBuddy -c \'Add :CFBundleLocalizations:5 string ja\' "${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}"`;
    `PlistBuddy -c \'Add :CFBundleLocalizations:6 string pt\' "${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}"`;
    `PlistBuddy -c \'Add :CFBundleLocalizations:7 string ru\' "${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}"`;
    `PlistBuddy -c \'Add :CFBundleLocalizations:8 string zh-Hans\' "${BUILT_PRODUCTS_DIR}/${INFOPLIST_PATH}"`;
}

my $codeSignIdentity =  $ENV{'EXPANDED_CODE_SIGN_IDENTITY'};
my $dylib = "$BUILT_PRODUCTS_DIR/" . $ENV{'FRAMEWORKS_FOLDER_PATH'} . "/Skillz.framework/Skillz";
my $signingPath = "$BUILT_PRODUCTS_DIR/" . $ENV{'FRAMEWORKS_FOLDER_PATH'} . "/Skillz.framework";
my $shouldSign = ($codeSignIdentity ne "") && (defined $codeSignIdentity) && -e "$signingPath";

print "Code sign identity: $codeSignIdentity \n";
print "Signing Path: $signingPath \n";

if ( $ENV{'DEPLOYMENT_LOCATION'} eq "YES") {
    my $fileResult = `file "$dylib"`;
    if (index($fileResult, "i386") != -1) {
        print "Exporting for release, remove unused archs. \n";
        my $tempfile = `mktemp -t skillz`;
        my $removearmv7s = "";
        
        if ($ENV{'ARCHS'} !~ /armv7s/) {
        	$removearmv7s = '-remove armv7s';
        	print "Arch armv7s not found in app, removing from Skillz.framework\n";
        }
        `/usr/bin/lipo -output "$tempfile" -remove i386 -remove x86_64 $removearmv7s "$dylib"`;
        `unlink "$dylib"`;
        `mv "$tempfile" "$dylib"`;
        print "Arch i386 found, removed \n";
    } else {
        print "Arch i386 not found \n";
    }
    print "Archiving or building for release, remove self \n";
    `rm "$0"`;

    if ($shouldSign) {
        print "Signing Skillz \n";
        `/usr/bin/codesign --force --verbose --sign "$codeSignIdentity" --preserve-metadata=identifier,entitlements,resource-rules "$signingPath"`;
    } else {
        print "No identity or no framework, not signing \n";
    }
} else {
    if ($shouldSign) {
        print "Signing Skillz \n";
        `/usr/bin/codesign --force --verbose --sign "$codeSignIdentity" --preserve-metadata=identifier,entitlements,resource-rules "$signingPath"`;
    } else {
        print "No identity or no framework, not signing \n";
    }
}
