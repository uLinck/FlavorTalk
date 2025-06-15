import 'package:flavortalk_app/widgets/typography/base_typography.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class FlavorHeading extends BaseTypography {
  const FlavorHeading({required super.text, super.key});

  @override
  Widget build(BuildContext context) {
    return Text(
      text,
      style: GoogleFonts.poppins(
        decoration: TextDecoration.none,
        color: Colors.black,
        fontSize: 40,
        fontWeight: FontWeight.w600,
      ),
    );
  }
}
